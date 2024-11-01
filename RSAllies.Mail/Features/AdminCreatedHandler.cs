﻿using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using RSAllies.Shared.Notifications;

namespace RSAllies.Mail.Features
{
    internal class AdminCreatedHandler : INotificationHandler<AdminCreated>
    {
        public async Task Handle(AdminCreated notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.Email))
            {
                try
                {
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.Connect("mail.privateemail.com", 465, true);
                        smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
                        message.To.Add(new MailboxAddress(notification.Username, notification.Email));
                        message.Subject = "Welcome to DCTTS Site Administration";
                        message.Body = new TextPart("plain")
                        {
                            Text = $"Hello, Join us at Site Administration " +
                                   $"your username is {notification.Username} and " +
                                   $"your password is {notification.Password}. " +
                                   $"Do not share your credentials "
                        };
                        await smtpClient.SendAsync(message, cancellationToken);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
