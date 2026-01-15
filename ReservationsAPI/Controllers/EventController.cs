using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Data;
using ReservationsAPI.Models;
using System;

namespace ReservationsAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly ReservationsAPIContext _context;
        public EventsController(ReservationsAPIContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int hallId)
        {
            var events = await _context.Events
                .Where(e => e.HallId == hallId)
                .OrderBy(e => e.StartsAt)
                .Select(e => new {
                    e.Id,
                    e.Name,
                    e.StartsAt,
                    e.HallId

                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest req)
        {
            var ev = new Event { HallId = req.HallId, Name = req.Name, StartsAt = req.StartsAt };
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return Ok(new { ev.Id });
        }
    }

    public record CreateEventRequest(int HallId, string Name, DateTime StartsAt);

}
