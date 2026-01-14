namespace ReservationsAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int HallSeatId { get; set; }
        public HallSeat HallSeat { get; set; } = null!;

        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
    }
}
