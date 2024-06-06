using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Requests;
using RSAllies.Users.Contracts.Responses;
using RSAllies.Users.Data;
using RSAllies.Users.Extensions;

namespace RSAllies.Users.Features.Authentication;

internal abstract class AuthenticateUser
{
    internal class Query : IRequest<Result<UserDto>>
    {
        public string Phone { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }

    internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var account = await context.Accounts
                .Where(ua => ua.Phone == request.Phone && ua.Password == request.Password && !ua.IsDeleted)
                .Include(ua => ua.User)
                .Select(ua => new UserDto
                {
                    Id = ua.User!.Id,
                    FirstName = ua.User.FirstName,
                    LastName = ua.User.LastName,
                    Email = ua.Email,
                    Phone = ua.Phone
                })
                .SingleOrDefaultAsync(cancellationToken);

            return account ?? Result.Failure<UserDto>(new Error("AuthenticateUser.NonExistentUser", "The specified user does not exist"));
        }
    }
}

public class AuthenticateUserEndPoint(ILogger<AuthenticateUserEndPoint> logger) : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/authenticate", async (AuthenticationDto user, ISender sender) =>
            {
                var request = new AuthenticateUser.Query
                {
                    Phone = user.Phone,
                    Password = user.Password
                };

                var result = await sender.Send(request);

                if (result.IsSuccess)
                {
                    logger.LogUserAuthenticated(user);
                }
                else
                {
                    logger.LogUserAuthenticationFailed(user);
                }

                return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
            })
            .Produces<Result<UserDto>>()
            .WithTags("Authentication");
    }
}