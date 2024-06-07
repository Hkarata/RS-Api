using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using RSAllies.Shared.Notifications;

namespace RSAllies.Mail.Features
{
    internal class AdminActivatedHandler(SmtpClient smtpClient) : INotificationHandler<AdminActivated>
    {
        public async Task Handle(AdminActivated notification, CancellationToken cancellationToken)
        {
            var Message = $"Hello, Your account has been activated at Site Administration " +
                          $"your username is {notification.Username} and " +
                          $"your password has not changed " +
                          $"Do not share your credentials ";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
            message.To.Add(new MailboxAddress(notification.Username, notification.Email));
            message.Subject = "Account Activation - Site Administration";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = Message
            };

            await smtpClient.SendAsync(message, cancellationToken);

        }
    }
}
