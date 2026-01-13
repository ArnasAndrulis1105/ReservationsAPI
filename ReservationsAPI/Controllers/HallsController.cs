using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Data;
using ReservationsAPI.Dto;
using ReservationsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        private readonly ReservationsAPIContext _context;

        public HallsController(ReservationsAPIContext context)
        {
            _context = context;
        }

        // GET: api/Halls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallDto>>> GetHalls()
        {
            var halls = await _context.Halls
                .Include(h => h.HallGroups)
                    .ThenInclude(g => g.HallSeats)
                .Select(h => new HallDto
                {
                    HallId = h.Id,
                    Name = h.Name,
                    HallGroups = h.HallGroups.Select(g => new HallGroupDto
                    {
                        GroupId = g.Id,
                        Name = g.Name,
                        HallSeats = g.HallSeats.Select(s => new HallSeatDto
                        {
                            ShowSeatId = s.Id,
                            Color = s.Color
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return Ok(halls);
        }


        // GET: api/Halls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallDto>> GetHall(int id)
        {
            var hall = await _context.Halls
                .Include(h => h.HallGroups)
                    .ThenInclude(g => g.HallSeats)
                .Where(h => h.Id == id)
                .Select(h => new HallDto
                {
                    HallId = h.Id,
                    Name = h.Name,
                    HallGroups = h.HallGroups.Select(g => new HallGroupDto
                    {
                        GroupId = g.Id,
                        Name = g.Name,
                        HallSeats = g.HallSeats.Select(s => new HallSeatDto
                        {
                            ShowSeatId = s.Id,
                            Color = s.Color
                        }).ToList()
                    }).ToList()
                })
                .SingleOrDefaultAsync();

            if (hall == null)
                return NotFound();

            return Ok(hall);
        }


        // PUT: api/Halls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHall(int id, Hall hall)
        {
            if (id != hall.Id)
            {
                return BadRequest();
            }

            _context.Entry(hall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(id))
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

        // POST: api/Halls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hall>> PostHall(Hall hall)
        {
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHall", new { id = hall.Id }, hall);
        }

        // DELETE: api/Halls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallExists(int id)
        {
            return _context.Halls.Any(e => e.Id == id);
        }
    }
}
