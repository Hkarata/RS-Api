using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Booking;
using RSAllies.Venues.Notifications;

namespace RSAllies.Venues.Features.Booking
{
    internal abstract class DeleteBooking
    {

        internal class Command : IRequest<Result>
        {
            public Guid BookingId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context, ILogger<Handler> logger, IMediator mediator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);

                if (booking == null)
                {
                    return Result.Failure(new Error("DeleteBooking.NonExistent", "The specified booking does not exist"));
                }

                booking.IsDeleted = true;

                booking.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                logger.LogInformation("Booking {BookingId} for user {UserId} has been deleted", request.BookingId, booking.UserId);

                await mediator.Publish(new BookingDeleted(booking.SessionId), cancellationToken);

                return Result.Success();
            }
        }

    }
}


public class DeleteBookingEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/booking/{bookingId:guid}", async (Guid bookingId, ISender sender) =>
        {
            var request = new DeleteBooking.Command { BookingId = bookingId };
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Booking");
    }
}