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
    internal abstract class GetVenueByRegion
    {
        internal class Query : IRequest<Result<List<SVenueDto>>>
        {
            public Guid RegionId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<List<SVenueDto>>>
        {
            public async Task<Result<List<SVenueDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var venues = await context.Venues
                    .AsNoTracking()
                    .Where(v => v.RegionId == request.RegionId)
                    .Include(v => v.District)
                    .Include(v => v.Region)
                    .Select(v => new SVenueDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Address = v.Address,
                        ImageUrl = v.ImageUrl,
                        Capacity = v.Capacity,
                        District = v.District!.Name,
                        Region = v.Region!.Name
                    })
                    .ToListAsync(cancellationToken);

                if (venues.Count == 0)
                {
                    return Result.Failure<List<SVenueDto>>(new Error("GetVenueByRegion.None",
                        "There are no venues in the specified region"));
                }

                return venues;
            }
        }
    }
}

public class GetVenueByRegionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/venues/region/{id:guid}", async (Guid id, ISender sender) =>
        {
            var request = new GetVenueByRegion.Query { RegionId = id };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<List<SVenueDto>>>()
            .WithTags("Venues");
    }
}