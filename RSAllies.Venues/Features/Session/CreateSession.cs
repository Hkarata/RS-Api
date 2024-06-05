﻿using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Extensions;
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

public class CreateSessionEndPoint(ILogger<CreateSessionEndPoint> logger) : ICarterModule
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

            if (result.IsSuccess)
            {
                logger.LogSessionCreated(session);
            }

            return Results.Ok(result);

        })
            .Produces<Result>()
            .WithTags("Session");
    }
}
