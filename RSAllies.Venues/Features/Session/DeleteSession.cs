using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Session;

namespace RSAllies.Venues.Features.Session
{
    internal class DeleteSession
    {
        internal class Command : IRequest<Result>
        {
            public Guid SessionId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context, IMediator mediator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var session = await context.Sessions
                    .Where(s => s.Id == request.SessionId && !s.IsDeleted)
                    .Include(s => s.Bookings)
                    .SingleOrDefaultAsync(cancellationToken);

                if (session == null)
                {
                    return Result.Failure(new Error("DeleteSession.NonExistent", "The specified session does not exist"));
                }

                session.IsDeleted = true;
                session.Bookings!.ForEach(b => b.IsDeleted = true);

                await mediator.Publish(new SessionDeleted(session.Id), cancellationToken);

                await context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}

public class DeleteSessionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/session/{sessionId:guid}", async (Guid sessionId, ISender sender) =>
        {
            var request = new DeleteSession.Command { SessionId = sessionId };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Session");
    }
}