using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.DataTypes;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Booking;

namespace RSAllies.Venues.Features.Booking
{
    internal abstract class ConfirmBooking
    {
        internal class Command : IRequest<Result>
        {
            public Guid BookingId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context, IMediator mediator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings
                    .Where(b => b.Id == request.BookingId && !b.IsDeleted)
                    .SingleOrDefaultAsync(cancellationToken);

                if (booking == null)
                {
                    return Result.Failure(new Error("ConfirmBooking.NonExistent", "The specified booking does not exist"));
                }


                booking.Status = BookingStatus.Confirmed;

                booking.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);

                await mediator.Publish(new BookingConfirmed(booking.UserId, booking.SessionId), cancellationToken);

                return Result.Success();
            }
        }
    }
}


public class ConfirmBookingEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/booking/{bookingId:guid}/confirm", async (Guid bookingId, ISender sender) =>
        {
            var request = new ConfirmBooking.Command { BookingId = bookingId };
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .WithTags("Booking")
            .Produces<Result>();
    }
}
