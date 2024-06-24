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
    public class TestPassAgeGroupAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/test-pass-age-group-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var testPassAgeGroupCounts = await context.TestPassAgeGroupCounts
                        .FromSqlRaw(Queries.TestPassAgedGroupAnalysis)
                        .ToListAsync(cancellationToken);

                if (testPassAgeGroupCounts.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<TestPassAgeGroupDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(testPassAgeGroupCounts));
            })
                .Produces<Result<List<TestPassAgeGroupDto>>>()
                .WithTags("Analysis");
        }
    }
}
