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
    public class EducationLevelCount : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/education-level-count", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var educationLevelCounts = await context.EducationLevelCounts
                    .FromSqlRaw(Queries.EducationLevelCount)
                    .OrderBy(x => x.Level)
                    .ToListAsync(cancellationToken);

                if (educationLevelCounts.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<EducationLevelDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(educationLevelCounts));
            })
                .Produces<Result<List<EducationLevelDto>>>()
                .WithTags("Analysis");
        }
    }
}
