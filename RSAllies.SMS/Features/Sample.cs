﻿using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Data;
using RSAllies.SMS.Queries;
using RSAllies.SMS.Services;

namespace RSAllies.SMS.Features
{
    public class Sample : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/text/{userId:guid}", async (Guid userId, SmsDbContext context, MessageService messageService) =>
            {

                var user = await GetUserDetailsAsync(userId, context);

                var message = $"Dear {user.Name}, your account has been created successfully. " +
                          $"You can now login to the Road Safety Allies platform using your phone number and password. " +
                          $"For any inquiries, please contact 0679844679 or support@roadsafetyallies.me. " +
                          $"   " +
                          $"Habari, {user.Name}, akaunti yako imefanikiwa kuundwa. " +
                          $"Sasa unaweza kuingia kwenye jukwaa la Road Safety Allies kwa kutumia namba ya simu na nywila. " +
                          $"Kwa maswali yoyote, tafadhali wasiliana na 0679844679 au support@roadsafetyallies.me.";

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


                return Results.Ok(user);
            });
        }

        internal async Task<User> GetUserDetailsAsync(Guid userId, SmsDbContext context)
        {
            var query = $"SELECT CONCAT(u.FirstName, ' ', u.MiddleName, ' ', u.LastName) AS Name, a.Phone AS Phone " +
                        $"FROM Users.Users u" +
                        $"Join Users.Accounts a ON u.Id = a.Id" +
                        $"WHERE u.Id = @UserId";

            var userIdParameter = new SqlParameter("@UserId", userId);

            var user = await context.Database
                .SqlQueryRaw<User>(query, userIdParameter)
                .FirstOrDefaultAsync();
            return user!;
        }
    }
}
