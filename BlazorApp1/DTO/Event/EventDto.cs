namespace BlazorApp1.DTO.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public string? Name { get; set; }
        public DateTime StartsAt { get; set; }
    }
}
