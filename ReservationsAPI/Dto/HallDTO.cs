using ReservationsAPI.Models;
using System.Xml.Serialization;

namespace ReservationsAPI.Dto
{
    public class HallDto
    {
        public int HallId { get; set; }
        public string Name { get; set; }
        public int TicketCount { get; set; }

        [XmlElement("HallGroup")]
        public List<HallGroupDto> HallGroups { get; set; }
    }

}
