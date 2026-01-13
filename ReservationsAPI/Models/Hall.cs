using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ReservationsAPI.Models
{

    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TicketCount { get; set; }

        public ICollection<HallGroup> HallGroups { get; set; }
    }



}
