using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.DataTypes;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Booking;
using RSAllies.Venues.Notifications;

namespace RSAllies.Venues.Features.Booking
{
    internal abstract class CreateBooking
    {
        internal class Command : IRequest<Result>
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context, IMediator mediator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var booking = new Entities.Booking
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    SessionId = request.SessionId,
                    BookedAt = DateTime.UtcNow,
                    Status = BookingStatus.Booked,
                    IsDeleted = false
                };

                context.Bookings.Add(booking);

                await context.SaveChangesAsync(cancellationToken);

                await mediator.Publish(new BookingMade(request.SessionId), cancellationToken);

                await mediator.Publish(new BookingNotification(request.UserId, request.SessionId), cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateBookingEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/booking", async (CreateBookingDto booking, ISender sender) =>
        {
            var request = new CreateBooking.Command { UserId = booking.UserId, SessionId = booking.SessionId };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Booking");
    }
}