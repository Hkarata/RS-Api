using RSAllies.Shared.Extensions;

namespace RSAllies.Shared.Requests
{
    public record struct AuthenticationDto(
        [PersonalIdentifiableInformation] string Phone,
        [SensitiveData] string Password
        );
}
