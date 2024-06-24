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
    public class BookingStatusCounts : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/booking-status-counts", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var statusCounts = await context.BookingStatusCounts
                    .FromSqlRaw(Queries.BookingStatusCount)
                    .ToListAsync(cancellationToken);

                if (statusCounts.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<BookingStatusCountDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(statusCounts));
            })
                .Produces<Result<List<BookingStatusCountDto>>>()
                .WithTags("Analysis");
        }
    }
}
