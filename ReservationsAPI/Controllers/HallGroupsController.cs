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
    public class HallGroupsController : ControllerBase
    {
        private readonly ReservationsAPIContext _context;

        public HallGroupsController(ReservationsAPIContext context)
        {
            _context = context;
        }

        // GET: api/HallGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallGroup>>> GetHallGroups()
        {
            return await _context.HallGroups
                .Include(hg => hg.HallSeats)
                .ToListAsync();
        }

        // GET: api/HallGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallGroup>> GetHallGroup(int id)
        {
            var hallGroup = await _context.HallGroups.Include(hg => hg.HallSeats).SingleOrDefaultAsync(hg => hg.GroupId == id);

            if (hallGroup == null)
            {
                return NotFound();
            }

            return hallGroup;
        }

        // PUT: api/HallGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHallGroup(int id, HallGroup hallGroup)
        {
            if (id != hallGroup.GroupId)
            {
                return BadRequest();
            }

            _context.Entry(hallGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallGroupExists(id))
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

        // POST: api/HallGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HallGroup>> PostHallGroup(HallGroup hallGroup)
        {
            _context.HallGroups.Add(hallGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHallGroup", new { id = hallGroup.GroupId }, hallGroup);
        }

        // DELETE: api/HallGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHallGroup(int id)
        {
            var hallGroup = await _context.HallGroups.FindAsync(id);
            if (hallGroup == null)
            {
                return NotFound();
            }

            _context.HallGroups.Remove(hallGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallGroupExists(int id)
        {
            return _context.HallGroups.Any(e => e.GroupId == id);
        }
    }
}
