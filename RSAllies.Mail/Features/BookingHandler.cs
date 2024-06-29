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
    internal class BookingHandler(EmailDbContext context) : INotificationHandler<BookingNotification>
    {
        public async Task Handle(BookingNotification notification, System.Threading.CancellationToken cancellationToken)
        {
            var user = await GetUserDetailsAsync(notification.UserId);

            if (!string.IsNullOrEmpty(user.Email))
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("mail.privateemail.com", 465, true);
                    smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

                    var venue = await GetVenueDetailsAsync(notification.SessionId);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
                    message.To.Add(new MailboxAddress(user.Name, user.Email));
                    message.Subject = "Booking Placement";
                    message.Body = new TextPart("plain")
                    {
                        Text = $"Dear {user.Name}, your booking for the session at {venue.VenueName} in {venue.District}, {venue.Region} " +
                               $"on {venue.Date:dd/MM/yyyy} from {venue.StartTime:HH:mm} to {venue.EndTime:HH:mm} has been booked. " +
                               $"For any inquiries, please contact 0679844679 or support@roadsafetyallies.me"
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
                .SingleOrDefaultAsync();

            return user!;
        }

        public async Task<Venue> GetVenueDetailsAsync(Guid sessionId)
        {
            var query = $"SELECT v.Name AS VenueName, v.Address AS VenueAddress, d.Name AS District, r.Name AS Region, " +
                        $"s.Date AS Date,s.StartTime AS StartTime, s.EndTime AS EndTime " +
                        $"FROM Venues.Sessions s JOIN Venues.Venues v ON s.VenueId = v.Id " +
                        $"JOIN Venues.Districts d ON v.DistrictId = d.Id " +
                        $"JOIN Venues.Regions r ON v.RegionId = r.Id " +
                        $"WHERE s.Id = @SessionId";
            var sessionIdParameter = new SqlParameter("@SessionId", sessionId);

            var venue = await context.Database
                .SqlQueryRaw<Venue>(query, sessionIdParameter)
                .SingleOrDefaultAsync();

            return venue!;
        }
    }
}
