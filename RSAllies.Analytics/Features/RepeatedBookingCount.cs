using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;

namespace RSAllies.Analytics.Features
{
    public class RepeatedBookingCount : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/repeated-bookings-count", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var RepeatedBookingCount = await context.RepeatedBookingCounts
                        .FromSqlRaw(Queries.TotalRepeatedBookingRate)
                        .FirstOrDefaultAsync(cancellationToken);


                if (RepeatedBookingCount!.RepeatedBookingCount == 0)
                {
                    return Results.Ok(Result.Failure<int>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(RepeatedBookingCount.RepeatedBookingCount));
            })
                .Produces<Result<int>>()
                .WithTags("Analysis");
        }
    }
}
