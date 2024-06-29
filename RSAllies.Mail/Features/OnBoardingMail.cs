using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using RSAllies.Mail.Messages;
using RSAllies.Shared.Notifications;

namespace RSAllies.Mail.Features;

internal class OnBoardingMail : INotificationHandler<AccountMade>
{
    public async Task Handle(AccountMade notification, CancellationToken cancellationToken)
    {
        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Connect("mail.privateemail.com", 465, true);
            smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

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
}