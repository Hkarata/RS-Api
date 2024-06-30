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
    internal class PaymentNumberCreatedHandler(EmailDbContext context) : INotificationHandler<PaymentNumberCreated>
    {
        public async Task Handle(PaymentNumberCreated notification, CancellationToken cancellationToken)
        {
            var user = await GetUserDetailsAsync(notification.UserId);

            if (!string.IsNullOrEmpty(user.Email))
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("mail.privateemail.com", 465, true);
                    smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

                    var englishMessage = $"Dear {user.Name}, your payment number is {notification.PaymentNumber}. Please use this number to make payment.";
                    var swahiliMessage = $"Habari {user.Name}, namba yako ya malipo ni {notification.PaymentNumber}. Tafadhali tumia namba hii kufanya malipo.";

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
                    message.To.Add(new MailboxAddress(user.Name, user.Email));
                    message.Subject = "Payment Number";
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
                        $"FROM Users.Users u "+
                        $"Join Users.Accounts a ON u.Id = a.Id " +
                        $"WHERE u.Id = @UserId";
            var userIdParameter = new SqlParameter("@UserId", userId);

            var user = await context.Database
                .SqlQueryRaw<User>(query, userIdParameter)
                .FirstOrDefaultAsync();

            return user!;
        }
    }
}
