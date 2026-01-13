using ReservationsAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ReservationsAPI.Models
{
    public class HallSeat
    {
        public int Id { get; set; }

        public int ShowSeatId { get; set; }
        public Color Color { get; set; }

        public bool IsReserved { get; set; }

        public int HallGroupId { get; set; }
        public HallGroup HallGroup { get; set; }
    }

}

