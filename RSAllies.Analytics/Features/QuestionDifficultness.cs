using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Contracts;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;

namespace RSAllies.Analytics.Features
{
    public class QuestionDifficultness : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/question-difficultness", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var questionDifficultness = await context.QuestionDifficulties
                        .FromSqlRaw(Queries.QuestionDifficultyLevel)
                        .ToListAsync(cancellationToken);

                if (questionDifficultness.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<QuestionDifficultyDto>>(Error.NullValue).Error);
                }
                return Results.Ok(Result.Success(questionDifficultness));
            })
                .Produces<Result<List<QuestionDifficultyDto>>>()
                .WithTags("Analysis");
        }
    }
}
