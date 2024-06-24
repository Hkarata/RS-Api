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
    public class LeastPopularVenues : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/least-popular-venues", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var venues = await context.LeastPopularVenues
                    .FromSqlRaw(Queries.LeastPopularVenues)
                    .ToListAsync(cancellationToken);

                if (venues.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<LeastPopularVenueDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(venues));
            })
                .Produces<Result<List<LeastPopularVenueDto>>>()
                .WithTags("Analysis");
        }
    }
}
