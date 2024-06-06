using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Responses;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Locale;

namespace RSAllies.Venues.Features.Locale
{
    internal abstract class GetRegions
    {
        internal class Query : IRequest<Result<List<RegionDto>>>
        {

        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<List<RegionDto>>>
        {
            public async Task<Result<List<RegionDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var regions = await context.Regions
                    .AsNoTracking()
                    .OrderBy(r => r.Name)
                    .Select(r => new RegionDto
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToListAsync(cancellationToken);

                if (regions.Count == 0)
                {
                    return Result.Failure<List<RegionDto>>(
                        new Error("GetRegions.None", "There are no regions")
                        );
                }

                return regions;
            }
        }
    }
}


public class GetRegionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/regions", async (ISender sender) =>
        {
            var request = new GetRegions.Query();
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result.Value);
        })
            .Produces<Result<List<RegionDto>>>()
            .WithTags("Locale");
    }
}