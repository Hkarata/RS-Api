using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;
using RSAllies.Test.Features;

namespace RSAllies.Test.Features
{
    internal abstract class CreateScore
    {
        internal class Command : IRequest<Result>
        {
            public Guid UserId { get; set; }
            public int ScoreValue { get; set; }
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var score = new Entities.Score
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    ScoreValue = request.ScoreValue,
                    CreatedAt = DateTime.UtcNow
                };

                context.Scores.Add(score);
                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}


public class CreateScoreEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/score", async (CreateScoreDto score, ISender sender) =>
        {
            var request = new CreateScore.Command
            {
                UserId = score.UserId,
                ScoreValue = score.ScoreValue
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Score");
    }
}