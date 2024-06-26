using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Responses;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Booking;

namespace RSAllies.Venues.Features.Booking
{
    internal abstract class GetBooking
    {
        internal class Query : IRequest<Result<BookingDto>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<BookingDto>>
        {
            public async Task<Result<BookingDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings
                    .AsNoTracking()
                    .Where(b => b.UserId == request.Id && !b.IsDeleted)
                    .OrderByDescending(b => b.BookedAt)
                    .Include(b => b.Session)
                    .ThenInclude(b => b!.Venue)
                    .ThenInclude(b => b!.Region)
                    .Select(b => new BookingDto
                    {
                        Id = b.Id,
                        BookedAt = b.Session!.Date,
                        Status = b.Status,
                        StartTime = b.Session!.StartTime,
                        EndTime = b.Session.EndTime,
                        VenueName = b.Session.Venue!.Name,
                        VenueAddress = b.Session.Venue.Address,
                        Region = b.Session.Venue.Region!.Name

                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (booking is null)
                {
                    return Result.Failure<BookingDto>(new Error("GetBooking.None",
                        "The specified user has no booking"
                        ));
                }

                return Result.Success(booking);

            }
        }
    }
}

public class GetBookingEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/booking/{userId:guid}", async (Guid userId, ISender sender) =>
        {
            var request = new GetBooking.Query { Id = userId };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<BookingDto>>()
            .WithTags("User");
    }
}
