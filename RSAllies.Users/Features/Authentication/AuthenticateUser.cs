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
using RSAllies.Users.Services;

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
            var query = await context.Accounts
                .Where(ua => ua.Phone == request.Phone && !ua.IsDeleted)
                .Include(ua => ua.User)
                .SingleOrDefaultAsync(cancellationToken);


            if (query is null)
            {
                return Result.Failure<UserDto>(
                    new Error("AuthenticateUser.NonExistentUser", "The specified user does not exist"));
            }

            if (!PasswordService.VerifyHashedPassword(query.Password, request.Password))
            {
                return Result.Failure<UserDto>(
                    new Error("AuthenticateUser.InvalidPassword", "The specified password is incorrect"));
            }

            var account = new UserDto
            {
                Id = query!.User!.Id,
                FirstName = query.User.FirstName,
                LastName = query.User.LastName,
                Email = query.Email,
                Phone = query.Phone
            };

            return account;
        }
    }
}

public class AuthenticateUserEndPoint : ICarterModule
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

                return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
            })
            .Produces<Result<UserDto>>()
            .WithTags("User");
    }
}