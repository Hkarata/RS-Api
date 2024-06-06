namespace RSAllies.Venues.Contracts.Responses
{
    public record DistrictDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
