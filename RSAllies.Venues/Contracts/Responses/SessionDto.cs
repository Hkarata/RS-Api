namespace RSAllies.Venues.Contracts.Responses
{
    public record SessionDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string District { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }


}
