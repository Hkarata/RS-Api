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
    public class PeakBookingMonths : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/peak-booking-months", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var peakBookingMonths = await context.PeakBookingMonths
                        .FromSqlRaw(Queries.PeakBookingMonths)
                        .ToListAsync(cancellationToken);

                if (peakBookingMonths.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<PeakBookingMonthDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(peakBookingMonths));
            })
                .Produces<Result<List<PeakBookingMonthDto>>>()
                .WithTags("Analysis");
        }
    }
}
