using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Data;
using RSAllies.Users.Entities;
using RSAllies.Users.Features.Admin;

namespace RSAllies.Users.Features.Admin
{
    internal abstract class CreateAdmin
    {
        internal class Command : IRequest<Result>
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public Guid RoleId { get; set; }
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingAdmin = await context.Administrators
                    .AnyAsync(a => a.Username == request.Username || a.Phone == request.Phone || a.Email == request.Email, cancellationToken);

                if (existingAdmin)
                {
                    return Result.Failure(new Error("CreateAdmin.Exists", "The specified admin already exists"));
                }

                var admin = new Administrator
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username,
                    Phone = request.Phone,
                    Email = request.Email,
                    Password = request.Password,
                    IsActive = true,
                    RoleId = request.RoleId
                };

                context.Administrators.Add(admin);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateAdminEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin", async (CreateAdminDto admin, ISender sender) =>
        {
            var request = new CreateAdmin.Command
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Username = admin.Username,
                Phone = admin.Phone,
                Email = admin.Email,
                Password = admin.Password,
                RoleId = admin.RoleId
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);

        });
    }
}