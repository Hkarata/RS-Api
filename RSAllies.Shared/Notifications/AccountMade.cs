using MediatR;

namespace RSAllies.Shared.Notifications;

public class AccountMade(Guid userId, string email) : INotification
{
    public Guid UserId { get; } = userId;
    public string Email { get; } = email;
}