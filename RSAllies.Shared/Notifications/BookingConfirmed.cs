using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class BookingConfirmed(Guid userId, Guid sessionId) : INotification
    {
        public Guid UserId { get; } = userId;
        public Guid SessionId { get; } = sessionId;
    }
}
