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
    internal abstract class GetDistricts
    {
        internal class Query : IRequest<Result<List<DistrictDto>>>
        {

        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<List<DistrictDto>>>
        {
            public async Task<Result<List<DistrictDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var districts = await context.Districts
                    .AsNoTracking()
                    .OrderBy(d => d.Name)
                    .Select(d => new DistrictDto
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToListAsync(cancellationToken);

                if (districts.Count == 0)
                {
                    return Result.Failure<List<DistrictDto>>(
                        new Error("GetDistricts.None", "There are no districts")
                        );
                }

                return districts;
            }
        }
    }
}


public class GetDistrictsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/districts", async (ISender sender) =>
        {
            var request = new GetDistricts.Query();
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result.Value);
        })
            .Produces<Result<List<DistrictDto>>>()
            .WithTags("Locale");
    }
}