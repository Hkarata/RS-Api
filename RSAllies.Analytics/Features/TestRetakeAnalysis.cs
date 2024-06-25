using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;

namespace RSAllies.Analytics.Features
{
    public class TestRetakeAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/test-retake", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var retakes = await context.TestRetakeCounts
                        .FromSqlRaw(Queries.TestRetakeCount)
                        .FirstOrDefaultAsync(cancellationToken);

                if (retakes!.RetakeCount == 0)
                {
                    return Results.Ok(Result.Failure<int>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(retakes.RetakeCount));
            })
                .Produces<Result<int>>()
                .WithTags("Analysis");
        }
    }
}
