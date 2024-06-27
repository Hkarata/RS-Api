using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Session;

namespace RSAllies.Venues.Features.Session
{
    internal abstract class CreateSession
    {
        internal class Command : IRequest<Result>
        {
            public DateTime Date { get; init; }
            public TimeOnly StartTime { get; init; }
            public TimeOnly EndTime { get; init; }
            public int Capacity { get; init; }
            public Guid VenueId { get; init; }
        }

        internal sealed class Handler(VenueDbContext context, IMediator mediator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var exist = await context.Sessions
                    .AnyAsync(s => s.Date == request.Date && s.StartTime == request.StartTime, cancellationToken);

                if (exist)
                {
                    return Result.Failure(new Error("CreateSession.Exists",
                        "A session already exists for the given date and start time"));
                }

                var session = new Entities.Session
                {
                    Id = Guid.NewGuid(),
                    Date = request.Date,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Capacity = request.Capacity,
                    VenueId = request.VenueId
                };

                context.Sessions.Add(session);

                await context.SaveChangesAsync(cancellationToken);

                await mediator.Publish(new SessionCreated(session.Id, session.Date), cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateSessionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/session", async (CreateSessionDto session, ISender sender) =>
        {
            var request = new CreateSession.Command
            {
                Date = session.Date,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                VenueId = session.VenueId
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);

        })
            .Produces<Result>()
            .WithTags("Session");
    }
}
