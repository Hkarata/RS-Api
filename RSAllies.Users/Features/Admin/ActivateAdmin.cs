using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Admin;

namespace RSAllies.Users.Features.Admin
{
    internal abstract class ActivateAdmin
    {
        internal class Command : IRequest<Result>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(UsersDbContext context, IMediator mediator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var admin = await context.Administrators
                    .SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                if (admin is null)
                {
                    return Result.Failure(new Error("ActivateAdmin.NotFound", "The specified admin does not exist"));
                }

                admin.IsActive = true;

                await context.SaveChangesAsync(cancellationToken);

                await mediator.Publish(new AdminActivated(admin.Username, admin.Phone, admin.Email), cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class ActivateAdminEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/admin/activate/{id}", async (Guid id, ISender sender) =>
        {
            var request = new ActivateAdmin.Command
            {
                Id = id
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result>()
            .WithTags("Admin");
    }
}
