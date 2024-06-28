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
    public class TestAgeGroupAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/test-age-group-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var analysis = await context.TestAgeGroups
                        .FromSqlRaw(Queries.TestAgeGroupAnalysis)
                        .ToListAsync(cancellationToken);

                if (analysis.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<TestAgeGroupDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(analysis));
            })
                .Produces<Result<List<TestAgeGroupDto>>>()
                .WithTags("Analysis");
        }
    }
}
