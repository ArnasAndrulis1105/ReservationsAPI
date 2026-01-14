namespace ReservationsAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;

        public string Name { get; set; } = "";
        public DateTime StartsAt { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }

}
