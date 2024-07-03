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
    internal class ReceivePhoneNumberHandler(SmsDbContext context, MessageService messageService) : INotificationHandler<ReceivePhoneNumbers>
    {
        public async Task Handle(ReceivePhoneNumbers notification, CancellationToken cancellationToken)
        {
            var venue = await GetVenueDetailsAsync(notification.SessionId);

            if (notification.PhoneNumbers.Count == 0)
            {
                return;
            }

            var message = $"Dear user, the session at {venue.VenueName} in {venue.District}, {venue.Region} " +
                          $"on {venue.Date:dd/MM/yyyy} from {venue.StartTime:HH:mm} to {venue.EndTime:HH:mm} has been cancelled. " +
                          $"For any inquiries, please contact 0679844679 or support@roadsafetyallies.me";

            // Send SMS
            var sms = new Sms
            {
                source_addr = "RSAllies",
                schedule_time = string.Empty,
                encoding = "0",
                message = message,
                recipients = new List<Recipient>()
            };

            int i = 1;

            foreach (var phoneNumber in notification.PhoneNumbers)
            {
                sms.recipients.Add(new Recipient { recipient_id = $"{i}", dest_addr = phoneNumber });
                i++;
            }

            await messageService.SendMessage(sms);

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
