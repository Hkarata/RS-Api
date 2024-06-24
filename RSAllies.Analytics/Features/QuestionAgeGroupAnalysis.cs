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
    public class QuestionAgeGroupAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/question-age-group-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var analysis = await context.QuestionAgeGroups
                        .FromSqlRaw(Queries.QuestionAgeGroupAnalysis)
                        .ToListAsync(cancellationToken);

                if (analysis.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<QuestionAgeGroupDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(analysis));
            })
                .Produces<Result<List<QuestionAgeGroupDto>>>()
                .WithTags("Analysis");
        }
    }
}
