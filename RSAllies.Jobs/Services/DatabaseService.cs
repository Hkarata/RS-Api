using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RSAllies.Jobs.Data;
using RSAllies.Jobs.Queries;

namespace RSAllies.Jobs.Services
{
    internal class DatabaseService(JobsDbContext context)
    {
        public async Task<List<string>> GetUsersID(Guid sessionId)
        {
            var firstQuery = $"SELECT b.UserId AS UserId " +
                             $"FROM Venues.Bookings b " +
                             $"WHERE b.SessionId = @SessionId AND b.Status = 2 AND b.IsDeleted = 0";

            var sessionIdParameter = new SqlParameter("@SessionId", sessionId);

            List<string> userIds = await context.Database
                .SqlQueryRaw<string>(firstQuery, sessionIdParameter)
                .ToListAsync();

            var users = userIds;

            return users;
        }

        public async Task<Venue> GetVenueDetails(Guid sessionId, CancellationToken cancellationToken)
        {
            string secondQuery = $"SELECT s.Date AS Date, s.Capacity AS Capacity, v.Name AS VenueName, v.Address AS VenueAddress, d.Name AS District, r.Name AS Region " +
                                 $"FROM Venues.Sessions s " +
                                 $"JOIN Venues.Venues v ON s.VenueId = v.Id " +
                                 $"JOIN Venues.Districts d ON v.DistrictId = d.Id " +
                                 $"JOIN Venues.Regions r ON v.RegionId = r.Id " +
                                 $"WHERE s.Id = @SessionId";

            var sessionIdParameter = new SqlParameter("@SessionId", sessionId);

            var details = await context.Database
                .SqlQueryRaw<Venue>(secondQuery, sessionIdParameter)
                .FirstOrDefaultAsync(cancellationToken);

            return details!;
        }

        public async Task<List<User>> GetUserDetails(Guid sessionId, CancellationToken cancellationToken)
        {
            var usersID = await GetUsersID(sessionId);

            var users = new List<User>();

            foreach (var UserId in usersID)
            {
                string thirdQuery = $"SELECT u.FirstName, u.MiddleName, u.LastName, u.Identification, g.GenderType as Gender" +
                                    $"FROM Users.Users u " +
                                    $"JOIN Users.Genders g ON u.GenderId = g.Id " +
                                    $"WHERE u.Id = @UserId";

                var userIdParameter = new SqlParameter("@UserId", UserId);

                var user = await context.Database
                    .SqlQueryRaw<User>(thirdQuery, userIdParameter)
                    .FirstOrDefaultAsync(cancellationToken);

                users.Add(user!);
            }

            return users;
        }

        public async Task CancelUnconfirmedBookings(Guid sessionId, CancellationToken cancellationToken)
        {
            // cancel all bookings for a specific session whose status is not confirmed
            string fourthQuery = $"UPDATE Venues.Bookings " +
                                 $"SET Status = CASE " +
                                 $"WHEN Status <> 2 THEN 3 "+
                                 $"ELSE Status " +
                                 $"END " +
                                 $"WHERE SessionId = @SessionId";

            var sessionIdParameter = new SqlParameter("@SessionId", sessionId);

            await context.Database
                .ExecuteSqlRawAsync(fourthQuery, sessionIdParameter, cancellationToken);
        }
    }
}
