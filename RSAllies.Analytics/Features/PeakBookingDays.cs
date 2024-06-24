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
    public class PeakBookingDays : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/peak-booking-days", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var peakBookingDays = await context.PeakBookingDays
                    .FromSqlRaw(Queries.PeakBookingDays)
                    .ToListAsync(cancellationToken);

                if (peakBookingDays.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<PeakBookingDaysDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(peakBookingDays));
            })
                .Produces<Result<List<PeakBookingDaysDto>>>()
                .WithTags("Analysis");
        }
    }
}
