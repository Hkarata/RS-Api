namespace RSAllies.Venues.Contracts.Requests;

public record CreateDistrictDto
{
    public Guid Id { get; set; }
    public string District { get; set; } = string.Empty;
}