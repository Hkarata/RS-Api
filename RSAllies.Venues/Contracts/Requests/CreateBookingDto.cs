namespace RSAllies.Venues.Contracts.Requests
{
    public record struct CreateBookingDto(
        Guid UserId,
        Guid SessionId
        );
}
