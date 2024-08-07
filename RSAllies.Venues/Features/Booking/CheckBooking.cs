﻿using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Booking;

namespace RSAllies.Venues.Features.Booking
{
    internal class CheckBooking
    {
        internal class Query : IRequest<Result<TimeSpan>>
        {
            public Guid UserId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<TimeSpan>>
        {
            public async Task<Result<TimeSpan>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Check if the user has any latest booking
                var bookings = await context.Bookings
                    .AsNoTracking()
                    .Include(b => b.Session)
                    .Where(b => b.UserId == request.UserId && !b.IsDeleted)
                    .OrderByDescending(b => b.BookedAt)
                    .FirstOrDefaultAsync(cancellationToken);
                // If the user has a booking and the session has not started
                if (bookings is not null && bookings.Session!.Date > DateTime.Now)
                {
                    // lets get the timespan left to the session
                    var timeSpan = bookings.Session.Date - DateTime.Now;
                    return timeSpan;
                }

                // If the user has no bookings, return a failure result

                return Result.Failure<TimeSpan>(new Error("CheckBooking.NoBooking", "The user has no bookings"));
            }
        }

    }
}

public class CheckBookingEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/check-booking/{userId:guid}", async (Guid userId, ISender sender) =>
        {
            var request = new CheckBooking.Query { UserId = userId };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<TimeSpan>>()
            .WithTags("Checks");
    }
}
