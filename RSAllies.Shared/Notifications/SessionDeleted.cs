using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class SessionDeleted(Guid sessionId) : INotification
    {
        public Guid SessionId { get; } = sessionId;
    }
}
