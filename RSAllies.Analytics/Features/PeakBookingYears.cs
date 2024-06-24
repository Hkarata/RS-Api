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
    public class PeakBookingYears : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/peak-booking-years", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var peakBookingYears = await context.PeakBookingYears
                    .FromSqlRaw(Queries.PeakBookingYears)
                    .OrderByDescending(x => x.Year)
                    .ToListAsync(cancellationToken);

                if (peakBookingYears.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<PeakBookingYearDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(peakBookingYears));
            })
                .Produces<Result<List<PeakBookingYearDto>>>()
                .WithTags("Analysis");
        }
    }
}
