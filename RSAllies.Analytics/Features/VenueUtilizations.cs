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
    public class VenueUtilizations : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/venue-utilizations", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var utilizations = await context.VenueUtilizations
                    .FromSqlRaw(Queries.VenueUtilizationRate)
                    .ToListAsync(cancellationToken);

                if (utilizations.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<VenueUtilizationDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(utilizations));
            })
                .Produces<Result<List<VenueUtilizationDto>>>()
                .WithTags("Analysis");
        }
    }
}
