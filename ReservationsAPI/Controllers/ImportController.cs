using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Data;
using ReservationsAPI.Dto;
using ReservationsAPI.Models;
using System;
using System.Xml.Serialization;

namespace ReservationsAPI.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController : ControllerBase
    {
        private readonly ReservationsAPIContext _context;

        public ImportController(ReservationsAPIContext context)
        {
            _context = context;
        }

        [HttpPost("xml")]
        public async Task<IActionResult> ImportXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            FilharmonijaDto data;

            var serializer = new XmlSerializer(typeof(FilharmonijaDto));
            using (var stream = file.OpenReadStream())
            {
                data = (FilharmonijaDto)serializer.Deserialize(stream);
            }

            var existingHall = await _context.Halls
                   .Include(h => h.HallGroups)
                       .ThenInclude(g => g.HallSeats)
                   .FirstOrDefaultAsync(h => h.Id == data.Hall.HallId);

            if (existingHall != null)
            {
                // Remove existing hall and all related data
                _context.Halls.Remove(existingHall);
                await _context.SaveChangesAsync();
            }

            var hall = new Hall
            {
                Name = data.Hall.Name,
                TicketCount = data.Hall.TicketCount,
                HallGroups = data.Hall.HallGroups.Select(g => new HallGroup
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    AZ = g.AZ,
                    HallSeats = g.HallSeats.Select(s => new HallSeat
                    {
                        ShowSeatId = s.ShowSeatId,
                        Color = s.Color,
                        IsReserved = s.IsReserved
                    }).ToList()
                }).ToList()
            };

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();

            return Ok("XML imported successfully");
        }
    }

}
