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
    internal class GetUserBookings
    {
        internal class Query : IRequest<Result<List<BookingDto>>>
        {
            public Guid UserId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<List<BookingDto>>>
        {
            public async Task<Result<List<BookingDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Get all bookings for the user which are not deleted, associated with the session and venue
                var bookings = await context.Bookings
                    .AsNoTracking()
                    .Include(b => b.Session)
                    .ThenInclude(s => s!.Venue)
                    .Where(b => b.UserId == request.UserId && !b.IsDeleted)
                    .Select(b => new BookingDto
                    {
                        Id = b.Id,
                        SessionId = b.Session!.Id,
                        BookedAt = b.BookedAt,
                        Status = b.Status,
                        StartTime = b.Session!.StartTime,
                        EndTime = b.Session!.EndTime,
                        VenueName = b.Session!.Venue!.Name,
                        VenueAddress = b.Session!.Venue!.Address,
                        Region = b.Session!.Venue!.Region!.Name
                    })
                    .ToListAsync(cancellationToken);

                // return failure result if the user has no bookings
                if (bookings.Count == 0)
                {
                    return Result.Failure<List<BookingDto>>(new Error("GetUserBookings.NoBookings", "The user has no bookings"));
                }


                return Result.Success(bookings);
            }
        }
    }
}

public class GetUserBookingsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/user-bookings/{userId:guid}", async (Guid userId, ISender sender) =>
        {
            var request = new GetUserBookings.Query { UserId = userId };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<List<BookingDto>>>()
            .WithTags("Bookings");
    }
}