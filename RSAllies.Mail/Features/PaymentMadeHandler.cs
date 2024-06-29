using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using RSAllies.Mail.Data;
using RSAllies.Mail.Queries;
using RSAllies.Shared.Notifications;

namespace RSAllies.Mail.Features
{
    internal class PaymentMadeHandler(EmailDbContext context) : INotificationHandler<PaymentMade>
    {
        public async Task Handle(PaymentMade notification, CancellationToken cancellationToken)
        {
            var user = await GetUserDetailsAsync(notification.UserId);

            if (!string.IsNullOrEmpty(user.Email))
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("mail.privateemail.com", 465, true);
                    smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

                    var englishMessage = $"Dear {user.Name}, your payment with number {notification.PaymentNumber} has been received. Thank you for your payment.";
                    var swahiliMessage = $"Habari {user.Name}, malipo yako yenye namba {notification.PaymentNumber} yamepokelewa. Asante kwa malipo yako.";


                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
                    message.To.Add(new MailboxAddress(user.Name, user.Email));
                    message.Subject = $"Successful Payment for {notification.PaymentNumber}";
                    message.Body = new TextPart("plain")
                    {
                        Text = englishMessage + " " + swahiliMessage
                    };

                    await smtpClient.SendAsync(message, cancellationToken);
                }
                    

            }
        }

        public async Task<User> GetUserDetailsAsync(Guid userId)
        {
            var query = $"SELECT CONCAT(u.FirstName, ' ', u.MiddleName, ' ', u.LastName) AS Name, a.Email AS Email " +
                        $"FROM Users.Users u JOIN Users.Accounts a ON u.Id = @UserId";
            var userIdParameter = new SqlParameter("@UserId", userId);

            var user = await context.Database
                .SqlQueryRaw<User>(query, userIdParameter)
                .FirstOrDefaultAsync();

            return user!;
        }
    }
}
