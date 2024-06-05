using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Contracts.Responses;
using RSAllies.Test.Data;
using RSAllies.Test.Features;

namespace RSAllies.Test.Features
{
    internal abstract class GetScore
    {
        internal class Query : IRequest<Result<ScoreDto>>
        {
            public Guid UserId { get; set; }
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result<ScoreDto>>
        {
            public async Task<Result<ScoreDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var score = await context.Scores
                    .Where(x => x.UserId == request.UserId)
                    .Select(x => new ScoreDto
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        ScoreValue = x.ScoreValue,
                        CreatedAt = x.CreatedAt
                    })
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefaultAsync(cancellationToken);

                if (score == null)
                {
                    return Result.Failure<ScoreDto>(new Error("GetScores.None",
                        "The specified user has no score"));
                }

                return score;
            }
        }
    }
}


public class GetScoreEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/score/{id:guid}", async (Guid id, ISender sender) =>
        {
            var request = new GetScore.Query
            {
                UserId = id
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result<ScoreDto>>()
            .WithTags("Score");
    }
}