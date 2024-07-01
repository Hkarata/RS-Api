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
    public class CaseAnalysis : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/case-analysis", async (AnalyticsDbContext context) =>
            {
                var counts = await context.Cases
                    .FromSqlRaw(Queries.CaseAnalysis)
                    .SingleOrDefaultAsync();

                if (counts!.OpenCases == 0)
                {
                    Results.Ok(Result.Failure<CaseDto>(Error.NullValue).Error);
                }

                return Results.Ok(Result.Success(counts));
            })
                .WithTags("Analysis");
        }
    }
}
