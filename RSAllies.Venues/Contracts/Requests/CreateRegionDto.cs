namespace RSAllies.Venues.Contracts.Requests
{
    public record CreateRegionDto
    {
        public Guid Id { get; set; }
        public string region { get; set; } = string.Empty;
    }
}
