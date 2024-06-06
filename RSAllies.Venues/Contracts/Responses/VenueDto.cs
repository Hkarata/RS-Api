namespace RSAllies.Venues.Contracts.Responses
{
    public record VenueDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<SessionDto>? Sessions { get; set; }
    }

}
