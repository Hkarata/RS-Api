using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Session;

namespace RSAllies.Venues.Features.Session
{
    internal abstract class CreateSessions
    {
        internal class Command : IRequest<Result>
        {
            public required List<Session> Sessions { get; set; }

            internal class Session
            {
                public DateTime Date { get; init; }
                public TimeOnly StartTime { get; init; }
                public TimeOnly EndTime { get; init; }
                public Guid VenueId { get; init; }
            }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var sessions = request.Sessions.Select(s => new Entities.Session
                {
                    Id = Guid.NewGuid(),
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    VenueId = s.VenueId
                }).ToList();

                context.Sessions.AddRange(sessions);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateSessionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/sessions", async (List<CreateSessionDto> sessions, ISender sender) =>
        {
            if (sessions.Count == 0)
            {
                return Results.Ok(
                    Result.Failure(new Error("Empty List", "No sessions in the list"))
                    );
            }

            var request = new CreateSessions.Command
            {
                Sessions = sessions.Select(s => new CreateSessions.Command.Session
                {
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    VenueId = s.VenueId
                }).ToList()
            };

            var result = await sender.Send(request);

            return Results.Ok(result);

        })
            .Produces<Result>()
            .WithTags("Sessions");
    }
}
