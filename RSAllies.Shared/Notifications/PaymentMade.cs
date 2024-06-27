using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class PaymentMade(Guid userId, string paymentNumber) : INotification
    {
        public Guid UserId { get; } = userId;
        public string PaymentNumber { get; } = paymentNumber;
    }
}
