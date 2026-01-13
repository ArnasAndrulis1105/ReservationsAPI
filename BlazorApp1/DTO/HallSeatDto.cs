namespace BlazorApp1.DTO
{
    public class HallSeatDto
    {
        public int HallGroupId { get; set; }
        public int ShowSeatId { get; set; }       // matches JSON
        public int Color { get; set; }
        public decimal Price { get; set; }
        public int SeatRow { get; set; }
        public string? SeatRowLetter { get; set; }
        public int SeatNumber { get; set; }
        public string? SeatNumberLetter { get; set; }
        public bool IsReserved { get; set; }
    }

}
