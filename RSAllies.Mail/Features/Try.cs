using Carter;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MimeKit;
using RSAllies.Mail.Messages;

namespace RSAllies.Mail.Features
{
    public class Try(SmtpClient smtpClient) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/try", async (string name, string email, CancellationToken cancellationToken) =>
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
                message.To.Add(new MailboxAddress(name, email));
                message.Subject = "Karibu katika Mfumo wa kupima Nadharia ya Udereva | Welcome to Driver-Centric Theoretical System";
                message.Body = new TextPart("plain")
                {
                    Text = OnBoardingMessage.GetOnBoardingMessage(name)
                };

                await smtpClient.SendAsync(message, cancellationToken);
            });
        }
    }
}
