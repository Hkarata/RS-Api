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
    internal class BookingConfirmedHandler(MessageService messageService, SmsDbContext context) : INotificationHandler<BookingConfirmed>
    {
        public async Task Handle(BookingConfirmed notification, CancellationToken cancellationToken)
        {
            var user = await GetUserDetailsAsync(notification.UserId);
            var venue = await GetVenueDetailsAsync(notification.SessionId);

            var message = $"Dear {user.Name}, your booking for the session at {venue.VenueName} in {venue.District}, {venue.Region} " +
                          $"on {venue.Date:dd/MM/yyyy} from {venue.StartTime:HH:mm} to {venue.EndTime:HH:mm} has been confirmed. " +
                          $"For any inquiries, please contact 0679844679 or support@roadsafetyallies.me ";


            // Send SMS
            var phone = 255 + user.Phone.TrimStart('0');

            var sms = new Sms
            {
                source_addr = "RSAllies",
                schedule_time = string.Empty,
                encoding = "0",
                message = message,
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
                        $"FROM Users.Users u JOIN Users.Accounts a ON u.Id = @UserId";
            var userIdParameter = new SqlParameter("@UserId", userId);

            var user = await context.Database
                .SqlQueryRaw<User>(query, userIdParameter)
                .FirstOrDefaultAsync();

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
                .FirstOrDefaultAsync();

            return venue!;
        }
    }
}
