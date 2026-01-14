namespace BlazorApp1.DTO.Event
{
    public class EventSeatDto
    {
        public int HallSeatId { get; set; }
        public int ShowSeatId { get; set; }
        public int Color { get; set; }
        public string? GroupName { get; set; }
        public bool IsReserved { get; set; }
    }
}
