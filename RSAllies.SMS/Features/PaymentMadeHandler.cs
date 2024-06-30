using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.Notifications;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Data;
using RSAllies.SMS.Queries;
using RSAllies.SMS.Services;

namespace RSAllies.SMS.Features
{
    internal class PaymentMadeHandler(SmsDbContext context, MessageService messageService) : INotificationHandler<PaymentMade>
    {
        public async Task Handle(PaymentMade notification, CancellationToken cancellationToken)
        {
            var user = await GetUserDetailsAsync(notification.UserId);

            var englishMessage = $"Dear {user.Name}, your payment with number {notification.PaymentNumber} has been received. Thank you for your payment.";
            var swahiliMessage = $"Habari {user.Name}, malipo yako yenye namba {notification.PaymentNumber} yamepokelewa. Asante kwa malipo yako.";

            // Send SMS
            var phone = 255 + user.Phone.TrimStart('0');

            var sms = new Sms
            {
                source_addr = "RSAllies",
                schedule_time = string.Empty,
                encoding = "0",
                message = englishMessage + " " + swahiliMessage,
                recipients = new List<Recipient>
                {
                    new Recipient { recipient_id = "1", dest_addr = phone}
                }
            };

            await messageService.SendMessage(sms);
        }

        public async Task<User> GetUserDetailsAsync(Guid userId)
        {
            var query = $"SELECT CONCAT(u.FirstName, ' ', u.MiddleName, ' ', u.LastName) AS Name, a.Phone AS Phone " +
                        $"FROM Users.Users u " +
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
