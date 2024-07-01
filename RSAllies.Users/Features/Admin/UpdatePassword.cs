using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Admin;

namespace RSAllies.Users.Features.Admin
{
    internal class UpdatePassword
    {
        internal class Command : IRequest<Result>
        {
            public Guid AdminId { get; set; }
            public string Password { get; set; } = string.Empty;
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var admin = await context.Administrators
                    .Where(a => a.Id == request.AdminId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (admin is null)
                {
                    return Result.Failure(new Error("UpdatePassword.NotFound", "Admin not found"));
                }

                admin.Password = request.Password;
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}

public class UpdatePasswordEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin/update-password", async (UpdatePasswordDto update, ISender sender) =>
        {
            var request = new UpdatePassword.Command
            {
                AdminId = update.UserId,
                Password = update.Password
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result>()
            .WithTags("Admin");
    }
}