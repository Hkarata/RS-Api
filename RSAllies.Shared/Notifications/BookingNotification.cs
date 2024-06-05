using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class BookingNotification(Guid UserId, Guid SessionId) : INotification
    {
        public Guid UserId { get; } = UserId;
        public Guid SessionId { get; } = SessionId;
    }
}
