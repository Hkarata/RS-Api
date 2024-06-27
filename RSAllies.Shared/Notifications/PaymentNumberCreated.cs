using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class PaymentNumberCreated(Guid userId, string paymentNumber) : INotification
    {
        public Guid UserId { get; } = userId;
        public string PaymentNumber { get; } = paymentNumber;
    }
}
