using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class ReceivePhoneNumbers(Guid sessionId, List<string> phoneNumbers) : INotification
    {
        public Guid SessionId { get; } = sessionId;
        public List<string> PhoneNumbers { get; } = phoneNumbers;
    }
}
