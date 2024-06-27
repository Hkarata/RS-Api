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

                var notification = new PaymentMade(userId, paymentNumber);

                await mediator.Publish(notification);

                return Results.Ok(result);
            })
                .WithTags("Payment");
        }
    }
}
