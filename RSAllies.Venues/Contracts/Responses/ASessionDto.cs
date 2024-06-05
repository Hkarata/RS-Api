namespace RSAllies.Venues.Contracts.Responses
{
    public record ASessionDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Capacity { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string VenueAddress { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }


}
