using Carter;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Test.Contracts.Requests;

namespace RSAllies.Test.Features.Answers
{
    public class RecordAnswers : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/user-responses", async (UserResponseDto response, IBus bus) =>
            {
                await bus.Publish(response);

                return Results.Ok();
            })
              .WithTags("Test");
        }
    }
}
