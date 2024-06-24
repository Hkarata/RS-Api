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
    public class PeakBookingTime : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/peak-booking-times", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var peakBookings = await context.PeakBookings
                    .FromSqlRaw(Queries.PeakBookingTimes)
                    .OrderBy(pb => pb.BookingHour)
                    .ToListAsync(cancellationToken);

                if (peakBookings.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<PeakBookingTimesDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(peakBookings));
            })
                .Produces<Result<List<PeakBookingTimesDto>>>()
                .WithTags("Analysis");
        }
    }
}
