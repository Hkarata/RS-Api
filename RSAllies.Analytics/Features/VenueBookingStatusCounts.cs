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
    public class VenueBookingStatusCounts : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/venue-booking-status-counts", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var counts = await context.VenueBookingStatusCounts
                    .FromSqlRaw(Queries.BookingStatusCountByVenue)
                    .ToListAsync(cancellationToken);

                if (counts.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<VenueBookingStatusCount>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(counts));
            })
                .Produces<Result<List<VenueBookingStatusCount>>>()
                .WithTags("Analysis");
        }
    }
}
