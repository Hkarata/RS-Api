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
    public class ScoreAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/scores-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var scores = await context.Scores
                       .FromSqlRaw(Queries.ScoreAnalysis)
                       .ToListAsync(cancellationToken);

                if (scores.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<ScoresDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(scores));
            })
                .Produces<Result<List<ScoresDto>>>()
                .WithTags("Analysis");
        }
    }
}
