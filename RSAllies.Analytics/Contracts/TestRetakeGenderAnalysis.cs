using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;

namespace RSAllies.Analytics.Contracts
{
    public class TestRetakeGenderAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/test-retake-gender-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var analysis = await context.TestGenderCounts
                    .FromSqlRaw(Queries.TestRetakeCountByGender)
                    .ToListAsync(cancellationToken);

                if (analysis.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<TestGenderDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(analysis));
            })
                .Produces<Result<List<TestGenderDto>>>()
                .WithTags("Analysis");
        }
    }
}
