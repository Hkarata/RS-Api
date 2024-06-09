namespace RSAllies.Users.Contracts.Responses;

public record UserData
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string MiddleName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}