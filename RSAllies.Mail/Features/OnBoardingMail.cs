using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using RSAllies.Mail.Messages;
using RSAllies.Shared.Notifications;

namespace RSAllies.Mail.Features;

internal class OnBoardingMail(SmtpClient smtpClient) : INotificationHandler<AccountMade>
{
    public async Task Handle(AccountMade notification, CancellationToken cancellationToken)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
        message.To.Add(new MailboxAddress(notification.Email, notification.Email));
        message.Subject = "Karibu katika Mfumo wa kupima Nadharia ya Udereva | Welcome to Driver-Centric Theoretical System";
        message.Body = new TextPart("plain")
        {
            Text = OnBoardingMessage.GetOnBoardingMessage(notification.Email)
        };

        await smtpClient.SendAsync(message, cancellationToken);
    }
}