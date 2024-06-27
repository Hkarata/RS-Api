using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.Notifications;
using RSAllies.Venues.Data;
using RSAllies.Venues.Services;

namespace RSAllies.Venues.Features.Payments
{
    public class GetPaymentNumber : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/payment-number/{userId:guid}", async (Guid userId, VenueDbContext context, IMediator mediator) =>
            {
                var paymentNumber = PaymentService.GeneratePaymentNumber(context, userId);

                var notification = new PaymentNumberCreated(userId, paymentNumber);

                await mediator.Publish(notification);

                return Results.Ok(paymentNumber);
            })
                .WithTags("Payment");
        }
    }
}
