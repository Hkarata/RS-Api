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
    public class CancellationRates : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/cancellation-rates", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var rates = await context.CancellationRates
                    .FromSqlRaw(Queries.CancellationRate)
                    .ToListAsync(cancellationToken);

                if (rates.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<CancellationRateDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(rates));
            })
                .Produces<Result<List<CancellationRateDto>>>()
                .WithTags("Analysis");
        }
    }
}
