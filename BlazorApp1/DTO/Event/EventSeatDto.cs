namespace BlazorApp1.DTO.Event
{
    public class EventSeatDto
    {
        public int HallSeatId { get; set; }
        public int ShowSeatId { get; set; }
        public int Color { get; set; }
        public string? GroupName { get; set; }
        public bool IsReserved { get; set; }


        public int SeatRow { get; set; }
        public string? SeatRowLetter { get; set; }
        public int SeatNumber { get; set; }
        public string? SeatNumberLetter { get; set; }
    }
}
