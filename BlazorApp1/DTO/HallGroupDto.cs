namespace BlazorApp1.DTO
{
    public class HallGroupDto
    {
        public int GroupId { get; set; }          // matches JSON
        public int HallId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Az { get; set; }
        public List<HallSeatDto> HallSeats { get; set; } = new();
    }

}
