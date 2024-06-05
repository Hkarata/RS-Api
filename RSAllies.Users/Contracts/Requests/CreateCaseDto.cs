using RSAllies.Shared.Extensions;

namespace RSAllies.Shared.Requests
{
    public record struct CreateCaseDto(
        Guid UserId,
        [PersonalIdentifiableInformation] string Username,
        string Message);
}
