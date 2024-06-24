using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Contracts;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.Analytics.Features
{
    public class BookingRate : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/booking-rate", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var bookingRates = await context.BookingRates
                    .FromSqlRaw(Queries.BookingRate)
                    .OrderBy(v => v.VenueName)
                    .ToListAsync(cancellationToken);

                if (bookingRates.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<BookingRateDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(bookingRates));
            })
                .Produces<Result<List<BookingRateDto>>>()
                .WithTags("Analysis");
        }
    }
}
