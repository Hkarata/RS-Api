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
    public class ConfirmationRates : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/confirmation-rates", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var rates = await context.ConfirmationRates
                    .FromSqlRaw(Queries.ConfirmationRate)
                    .ToListAsync(cancellationToken);

                if (rates.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<ConfirmationRateDto>>(Error.NullValue));
                }


                return Results.Ok(Result.Success(rates));
            })
                .Produces<Result<List<ConfirmationRateDto>>>()
                .WithTags("Analysis");
        }
    }
}
