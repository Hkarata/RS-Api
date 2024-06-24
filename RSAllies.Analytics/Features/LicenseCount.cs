using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Contracts;
using RSAllies.Analytics.Data;
using RSAllies.Shared.HelperTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.Analytics.Features
{
    public class LicenseCount : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/license-count", async (AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var licenseCounts = await context.LicenseCounts
                    .FromSqlRaw(Queries.LicenseClassCount)
                    .ToListAsync(cancellationToken);

                if (licenseCounts.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<LicenseCount>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(licenseCounts));
            })
                .Produces<Result<List<LicenseDto>>>()
                .WithTags("Analysis");
        }
    }
}
