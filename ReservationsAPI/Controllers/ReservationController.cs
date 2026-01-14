using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Data;
using ReservationsAPI.Models;
using System;

namespace ReservationsAPI.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsAPIContext _context;
        public ReservationsController(ReservationsAPIContext context) => _context = context;

        [HttpGet("seats")]
        public async Task<IActionResult> GetSeats([FromQuery] int eventId)
        {
            var ev = await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == eventId);
            if (ev is null) return NotFound("Event not found");

            var seats = await _context.HallSeats
                .Where(s => s.HallGroup.HallId == ev.HallId)
                .Select(s => new
                {
                    HallSeatId = s.Id,              // svarbu turėti Id HallSeat entityje
                    s.ShowSeatId,
                    s.Color,
                    GroupName = s.HallGroup.Name,
                    IsReserved = _context.Reservations.Any(r => r.EventId == eventId && r.HallSeatId == s.Id)
                })
                .ToListAsync();

            return Ok(seats);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve([FromBody] CreateReservationRequest req)
        {
            // apsauga: rezervuojam tik jei dar neužimta
            var alreadyReserved = await _context.Reservations
                .Where(r => r.EventId == req.EventId && req.HallSeatIds.Contains(r.HallSeatId))
                .Select(r => r.HallSeatId)
                .ToListAsync();

            var toReserve = req.HallSeatIds.Except(alreadyReserved).ToList();
            if (toReserve.Count == 0)
                return BadRequest("Selected seats are already reserved.");

            foreach (var seatId in toReserve)
            {
                _context.Reservations.Add(new Reservation
                {
                    EventId = req.EventId,
                    HallSeatId = seatId
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // jei keli klientai vienu metu - suveiks unique index
                return Conflict("Some seats were reserved by someone else. Refresh and try again.");
            }

            return Ok(new { Reserved = toReserve.Count, Skipped = alreadyReserved.Count });
        }
    }

    public record CreateReservationRequest(int EventId, List<int> HallSeatIds);

}
