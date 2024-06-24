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
    public class QuestionAnalyses : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/question-analyses", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var questionAnalyses = await context.QuestionAnalyses
                        .FromSqlRaw(Queries.QuestionAnalysis)
                        .ToListAsync(cancellationToken);

                if (questionAnalyses.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<QuestionAnalysisDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(questionAnalyses));
            })
                .Produces<Result<List<QuestionAnalysisDto>>>()
                .WithTags("Analysis");
        }
    }
}
