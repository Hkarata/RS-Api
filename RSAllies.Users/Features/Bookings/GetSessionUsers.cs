using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Responses;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Bookings;

namespace RSAllies.Users.Features.Bookings
{
    internal abstract class GetSessionUsers
    {
        internal class Query : IRequest<Result<List<UserData>>>
        {
            public Guid SessionId { get; set; }
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<List<UserData>>>
        {
            public async Task<Result<List<UserData>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = $"SELECT DISTINCT u.Id, u.FirstName , u.MiddleName, u.LastName, a.Phone, a.Email " +
                            $"FROM [Users].[Users] u " +
                            $"INNER JOIN [Users].[Accounts] a ON u.Id = a.Id " +
                            $"INNER JOIN [Venues].[Bookings] b ON u.Id = b.UserId " +
                            $"WHERE b.SessionId = @SessionId";

                var sessionIdParameter = new SqlParameter("@SessionId", request.SessionId);

                var users = await context.Database
                    .SqlQueryRaw<UserData>(query, sessionIdParameter)
                    .ToListAsync(cancellationToken);

                if (users.Count == 0)
                {
                    return Result.Failure<List<UserData>>(
                        new Error("GetSessionUsers.None", "The specified session has no bookings")
                        );
                }

                return users;
            }
        }
    }
}

public class GetSessionUsersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessions/{sessionId:guid}/users", async (Guid sessionId, ISender sender) =>
        {
            var request = new GetSessionUsers.Query { SessionId = sessionId };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<List<UserData>>>()
            .WithTags("Sessions");
    }
}