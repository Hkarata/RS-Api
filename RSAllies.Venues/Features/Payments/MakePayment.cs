using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.DataTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Data;
using RSAllies.Venues.Services;

namespace RSAllies.Venues.Features.Payments
{
    public class MakePayment : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/payment", async (Guid userId, string paymentNumber, VenueDbContext context, IMediator mediator) =>
            {
                var result = PaymentService.ProcessPayment(context, paymentNumber);

                if (result.IsFailure)
                {
                    return Results.Ok(result.Error);
                }

                var booking = await context.Bookings
                    .Where(b => b.UserId == userId && b.Status == BookingStatus.Booked)
                    .OrderByDescending(b => b.BookedAt)
                    .FirstOrDefaultAsync();

                if (booking != null)
                {
                    booking.Status = BookingStatus.Paid;
                    booking.UpdatedAt = DateTime.UtcNow;

                    await context.SaveChangesAsync();
                }

                var notification = new PaymentMade(userId, paymentNumber);

                await mediator.Publish(notification);

                return Results.Ok(result);
            })
                .WithTags("Payment");
        }
    }
}
