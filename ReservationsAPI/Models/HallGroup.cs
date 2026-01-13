using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ReservationsAPI.Models
{
    public class HallGroup
    {

        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int AZ { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }

        public ICollection<HallSeat> HallSeats { get; set; }
    }
}
