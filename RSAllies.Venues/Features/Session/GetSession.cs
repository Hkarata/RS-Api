using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Responses;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Session;

namespace RSAllies.Venues.Features.Session
{
    internal abstract class GetSession
    {
        internal class Query : IRequest<Result<SessionDto>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<SessionDto>>
        {
            public async Task<Result<SessionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var session = await context.Sessions
                    .AsNoTracking()
                    .Where(s => s.Id == request.Id)
                    .Include(s => s.Venue)
                    .ThenInclude(s => s!.Region)
                    .Include(s => s.Venue!.District)
                    .Select(s => new SessionDto
                    {
                        Id = s.Id,
                        Date = s.Date,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        District = s.Venue!.District!.Name,
                        Region = s.Venue!.Region!.Name,
                        Capacity = s.Capacity
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (session is null)
                {
                    return Result.Failure<SessionDto>(new Error("GetSession.NonExistentSession",
                        "The specified session does not exist"));
                }

                return session;
            }
        }
    }
}

public class GetSessionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/session/{id:guid}", async (Guid id, ISender sender) =>
        {
            var request = new GetSession.Query { Id = id };
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .Produces<Result<SessionDto>>()
            .WithTags("Session");
    }
}

