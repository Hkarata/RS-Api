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
    public class GenderCount : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/gender-count", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var genderCounts = await context.GenderCounts
                    .FromSqlRaw(Queries.GenderCount)
                    .ToListAsync(cancellationToken);

                if (genderCounts.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<GenderDto>>(Error.NullValue).Error);
                }

                var result = Result.Success(genderCounts);

                return Results.Ok(result);
            })
                .Produces<Result<List<GenderCount>>>()
                .WithTags("Analysis");
        }
    }
}
