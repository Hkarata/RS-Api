using RSAllies.Shared.Extensions;

namespace RSAllies.Users.Contracts.Requests;

public record CreateAccountDto
{
    public Guid UserId { get; set; }

    [PersonalIdentifiableInformation]
    public string Phone { get; set; } = string.Empty;

    [PersonalIdentifiableInformation]
    public string Email { get; set; } = string.Empty;

    [PersonalIdentifiableInformation]
    public string Password { get; set; } = string.Empty;
}