using System.Xml.Serialization;
using ReservationsAPI.Enums;

namespace ReservationsAPI.Dto
{
    public class HallSeatDto
    {
        public int HallGroupId { get; set; }
        public int ShowSeatId { get; set; }

        public Color Color { get; set; }

        public decimal Price { get; set; }

        public int SeatRow { get; set; }
        public string SeatRowLetter { get; set; }

        public int SeatNumber { get; set; }
        public string SeatNumberLetter { get; set; }

        public bool IsReserved { get; set; }
    }


}
