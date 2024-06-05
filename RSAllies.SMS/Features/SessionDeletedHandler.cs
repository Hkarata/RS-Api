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
    internal class SessionDeletedHandler(SmsDbContext context, MessageService messageService) : INotificationHandler<SessionDeleted>
    {
        public async Task Handle(SessionDeleted notification, CancellationToken cancellationToken)
        {
            var venue = await GetVenueDetailsAsync(notification.SessionId);
            var phoneNumbers = await GetPhoneNumbers(notification.SessionId, cancellationToken);

            var message = $"Dear user, the session at {venue.VenueName} in {venue.District}, {venue.Region} " +
                          $"on {venue.Date:dd/MM/yyyy} from {venue.StartTime:HH:mm} to {venue.EndTime:HH:mm} has been cancelled. " +
                          $"For any inquiries, please contact 0679844679 or support@roadsafetyallies.me";

            // Send SMS

            var sms = new Sms
            {
                SourceAddress = "RSAllies",
                ScheduleTime = string.Empty,
                Encoding = "0",
                Message = message,
                Recipients = new List<Recipient>()
            };

            int i = 1;

            foreach (var phoneNumber in phoneNumbers)
            {
                sms.Recipients.Add(new Recipient { RecipientId = i, DestinationAddress = phoneNumber });
                i++;
            }

            await messageService.SendMessage(sms);
        }

        public async Task<List<string>> GetUsersID(Guid sessionId)
        {
            var firstQuery = $"SELECT b.UserId AS UserId " +
                             $"FROM Venues.Bookings b " +
                             $"WHERE b.SessionId = @SessionId AND b.Status = 2 AND b.IsDeleted = 0";

            var sessionIdParameter = new SqlParameter("@SessionId", sessionId);

            List<string> userIds = await context.Set<string>()
                .FromSqlRaw(firstQuery, sessionIdParameter).ToListAsync();

            var users = userIds;

            return users;
        }

        public async Task<List<string>> GetPhoneNumbers(Guid sessionId, CancellationToken cancellationToken)
        {
            var usersID = await GetUsersID(sessionId);

            var phoneNumbers = new List<string>();

            foreach (var UserId in usersID)
            {
                string thirdQuery = $"SELECT u.Phone AS Phone " +
                                    $"FROM Users.Users u " +
                                    $"WHERE u.Id = @UserId";

                var userIdParameter = new SqlParameter("@UserId", UserId);

                var number = await context.Set<string>()
                    .FromSqlRaw(thirdQuery, userIdParameter)
                    .FirstOrDefaultAsync(cancellationToken);

                var phoneNumber = "255" + number!.TrimStart('0');

                phoneNumbers.Add(phoneNumber);
            }

            return phoneNumbers;
        }

        public async Task<Venue> GetVenueDetailsAsync(Guid sessionId)
        {
            var query = $"SELECT v.Name AS VenueName, v.Address AS VenueAddress, d.Name AS District, r.Name AS Region, " +
                        $"s.Date AS Date,s.StartTime AS StartTime, s.EndTime AS EndTime " +
                        $"FROM Venues.Sessions s JOIN Venues.Venues v ON s.VenueId = v.Id " +
                        $"JOIN Venues.Districts d ON v.DistrictId = d.Id " +
                        $"JOIN Venues.Regions r ON v.RegionId = r.Id " +
                        $"WHERE s.SessionId = @SessionId";
            var sessionIdParameter = new SqlParameter("@SessionId", sessionId);

            var venue = await context.Set<Venue>().FromSqlRaw(query, sessionIdParameter).FirstOrDefaultAsync();
            return venue!;
        }
    }
}
