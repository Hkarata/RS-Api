using MediatR;

namespace RSAllies.Shared.Notifications
{
    public class AdminCreated(string username, string password, string phone, string email) : INotification
    {
        public string Email { get; } = email;
        public string Phone { get; } = phone;
        public string Username { get; } = username;
        public string Password { get; } = password;
    }
}
