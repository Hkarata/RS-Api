using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class AdminActivated(string username, string phone, string email) : INotification
    {
        public string Email { get; } = email;
        public string Phone { get; } = phone;
        public string Username { get; } = username;
    }
}
