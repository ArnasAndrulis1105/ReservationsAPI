using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Data;
using ReservationsAPI.Models;

namespace ReservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallSeatsController : ControllerBase
    {
        private readonly ReservationsAPIContext _context;

        public HallSeatsController(ReservationsAPIContext context)
        {
            _context = context;
        }

        // GET: api/HallSeats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallSeat>>> GetHallSeats()
        {
            return await _context.HallSeats.Include(s => s.HallGroup).ToListAsync();
        }

        // GET: api/HallSeats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallSeat>> GetHallSeat(int id)
        {
            var hallSeat = await _context.HallSeats.Include(s => s.HallGroup).SingleOrDefaultAsync(s => s.Id == id);

            if (hallSeat == null)
            {
                return NotFound();
            }

            return hallSeat;
        }

        // PUT: api/HallSeats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHallSeat(int id, HallSeat hallSeat)
        {
            if (id != hallSeat.ShowSeatId)
            {
                return BadRequest();
            }

            _context.Entry(hallSeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallSeatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HallSeats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HallSeat>> PostHallSeat(HallSeat hallSeat)
        {
            _context.HallSeats.Add(hallSeat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHallSeat", new { id = hallSeat.ShowSeatId }, hallSeat);
        }

        // DELETE: api/HallSeats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHallSeat(int id)
        {
            var hallSeat = await _context.HallSeats.FindAsync(id);
            if (hallSeat == null)
            {
                return NotFound();
            }

            _context.HallSeats.Remove(hallSeat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallSeatExists(int id)
        {
            return _context.HallSeats.Any(e => e.ShowSeatId == id);
        }
    }
}
