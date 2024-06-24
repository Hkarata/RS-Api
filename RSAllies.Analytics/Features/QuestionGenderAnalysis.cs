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
    public class QuestionGenderAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/question-gender-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var analysis = await context.QuestionGenderCounts
                        .FromSqlRaw(Queries.QuestionGenderAnalysis)
                        .ToListAsync(cancellationToken);

                if (analysis.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<QuestionGenderDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(analysis));
            })
                .Produces<Result<List<QuestionGenderDto>>>()
                .WithTags("Analysis");
        }
    }
}
