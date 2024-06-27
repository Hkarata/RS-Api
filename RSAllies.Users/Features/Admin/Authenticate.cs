using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Contracts.Responses;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Admin;
using RSAllies.Users.Services;

namespace RSAllies.Users.Features.Admin
{
    internal abstract class Authenticate
    {
        internal class Command : IRequest<Result<AdminDto>>
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Command, Result<AdminDto>>
        {
            public async Task<Result<AdminDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var query = await context.Administrators
                    .AsNoTracking()
                    .Where(a => a.Username == request.Username && a.Password == request.Password && a.IsActive)
                    .Include(a => a.Role)
                    .SingleOrDefaultAsync(cancellationToken);

                if (query is null)
                {
                    return Result.Failure<AdminDto>(new Error("AuthenticateAdmin.Failed", "invalid account"));
                }

                if (!PasswordService.VerifyHashedPassword(query.Password, request.Password))
                {
                    return Result.Failure<AdminDto>(new Error("AuthenticateAdmin.Failed", "invalid password"));
                }

                var admin = new AdminDto
                {
                    Id = query.Id,
                    Username = query.Username,
                    Role = query.Role!.Name
                };

                return admin;
            }
        }
    }
}

public class AuthenticateEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin/authenticate", async (AdminLogin admin, ISender sender) =>
        {
            var request = new Authenticate.Command
            {
                Username = admin.Username,
                Password = admin.Password
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<AdminDto>>()
            .WithTags("Admin");
    }
}
