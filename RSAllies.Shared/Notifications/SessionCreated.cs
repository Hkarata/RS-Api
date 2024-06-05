using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class SessionCreated(Guid sessionId, DateTime scheduledAt) : INotification
    {
        public Guid SessionId { get; } = sessionId;
        public DateTime ScheduledAt { get; } = scheduledAt;
    }
}
