using ReservationsAPI.Models;
using System.Xml.Serialization;

namespace ReservationsAPI.Dto
{
    public class HallGroupDto
    {
        public int GroupId { get; set; }
        public int HallId { get; set; }
        public string Name { get; set; }
        public int AZ { get; set; }

        [XmlElement("HallSeat")]
        public List<HallSeatDto> HallSeats { get; set; }
    }

}
