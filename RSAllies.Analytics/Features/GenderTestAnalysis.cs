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
    public class GenderTestAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/gender-test-analysis", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var genderTests = await context.GenderTests
                    .FromSqlRaw(Queries.GenderTestAnalysis)
                    .ToListAsync(cancellationToken);

                if (genderTests.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<GenderTestDto>>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(genderTests));
            })
                .Produces<Result<List<GenderTestDto>>>()
                .WithTags("Analysis");
        }
    }
}
