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
    public class TestRetakeAgeGroupAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/test-retake-age-group-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var analysis = await context.TestRetakeAgeGroupCounts
                    .FromSqlRaw(Queries.TestRetakeByAgeGroup)
                    .ToListAsync(cancellationToken);

                if (analysis.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<TestRetakeAgeGroupDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(analysis));
            })
                .Produces<Result<List<TestRetakeAgeGroupDto>>>()
                .WithTags("Analysis");
        }
    }
}
