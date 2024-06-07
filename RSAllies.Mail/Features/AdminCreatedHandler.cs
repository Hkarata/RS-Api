using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using RSAllies.Shared.Notifications;

namespace RSAllies.Mail.Features
{
    internal class AdminCreatedHandler(SmtpClient smtpClient) : INotificationHandler<AdminCreated>
    {
        public async Task Handle(AdminCreated notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.Email))
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
                message.To.Add(new MailboxAddress(notification.Username, notification.Email));
                message.Subject = "Welcome to DCTTS Site Administration";
                message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = $"Hello, Join us at Site Administration " +
                           $"your username is {notification.Username} and " +
                           $"your password is {notification.Password}. " +
                           $"Do not share your credentials "
                };
                await smtpClient.SendAsync(message, cancellationToken);
            }
        }
    }
}
