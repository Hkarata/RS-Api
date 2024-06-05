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
    internal abstract class GetSessionsByRegionAndDate
    {
        internal class Query : IRequest<Result<List<ASessionDto>>>
        {
            public string Region { get; set; } = string.Empty;
            public DateTime Date { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<List<ASessionDto>>>
        {
            public async Task<Result<List<ASessionDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sessions = await context.Sessions
                    .AsNoTracking()
                    .Where(s => !s.IsDeleted && s.Date >= request.Date)
                    .Include(s => s.Venue)
                    .ThenInclude(v => v!.Region)
                    .Where(s => s.Venue!.Region!.Name == request.Region)
                    .Select(s => new ASessionDto
                    {
                        Id = s.Id,
                        Date = s.Date,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        Capacity = s.Capacity,
                        VenueName = s.Venue!.Name,
                        VenueAddress = s.Venue.Address,
                        Region = s.Venue.Region!.Name
                    })
                    .ToListAsync(cancellationToken);

                if (sessions.Count == 0)
                {
                    return Result.Failure<List<ASessionDto>>(new Error("GetSessionsByRegionAndDate.None", "No sessions found."));
                }

                return sessions;
            }
        }
    }
}

public class GetSessionsByRegionAndDateEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessions/{region}/{date:datetime}", async (DateTime date, string region, ISender sender) =>
        {
            var request = new GetSessionsByRegionAndDate.Query { Date = date, Region = region };
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .Produces<Result<List<ASessionDto>>>()
            .WithTags("Sessions");
    }
}