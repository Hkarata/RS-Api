using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class GetPhoneNumbers(Guid sessionId, List<Guid> usersId) : INotification
    {
        public Guid SessionId { get; } = sessionId;
        public List<Guid> UsersId { get; } = usersId;
    }
}
