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
    public class AverageSessions : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/average-sessions", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var averageSessions = await context.AverageSessions
                    .FromSqlRaw(Queries.AverageSessions)
                    .OrderBy(x => x.VenueName)
                    .ToListAsync(cancellationToken);

                if (averageSessions.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<AverageSessionDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(averageSessions));
            })
                .Produces<Result<List<AverageSessionDto>>>()
                .WithTags("Analysis");
        }
    }
}
