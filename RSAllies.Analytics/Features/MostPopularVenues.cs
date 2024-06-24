using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Contracts;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;

namespace RSAllies.Analytics.Features
{
    public class MostPopularVenues : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/most-popular-venues", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var venues = await context.MostPopularVenues
                    .FromSqlRaw(Queries.MostPopularVenues)
                    .ToListAsync(cancellationToken);

                if (venues.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<MostPopularVenueDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(venues));
            })
                .Produces<Result<List<MostPopularVenueDto>>>()
                .WithTags("Analysis");
        }
    }
}
