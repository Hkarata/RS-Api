using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Responses;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Venue;

namespace RSAllies.Venues.Features.Venue
{
    internal abstract class GetVenues
    {
        internal class Query : IRequest<Result<List<SVenueDto>>>
        {

        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<List<SVenueDto>>>
        {
            public async Task<Result<List<SVenueDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var venues = await context.Venues
                    .Where(v => !v.IsDeleted)
                    .Include(v => v.District)
                    .Include(v => v.Region)
                    .Select(v => new SVenueDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Address = v.Address,
                        District = v.District!.Name,
                        Region = v.Region!.Name,
                        Capacity = v.Capacity
                    })
                    .ToListAsync(cancellationToken);

                if (venues.Count == 0)
                {
                    return Result.Failure<List<SVenueDto>>(new Error("GetVenues.None",
                        "The are no venues"));
                }

                return Result.Success(venues);
            }
        }
    }
}

public class GetVenuesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/venues", async (ISender sender) =>
        {
            var request = new GetVenues.Query();
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .Produces<Result<List<ASessionDto>>>()
            .WithTags("Venues");
    }
}