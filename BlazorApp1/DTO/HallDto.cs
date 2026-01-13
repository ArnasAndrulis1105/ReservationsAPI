namespace BlazorApp1.DTO
{
    public class HallDto
    {
        public int HallId { get; set; }           // matches JSON
        public string Name { get; set; } = string.Empty;
        public int TicketCount { get; set; }
        public List<HallGroupDto> HallGroups { get; set; } = new();
    }

}
