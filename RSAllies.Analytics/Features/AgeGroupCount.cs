﻿using Carter;
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
    public class AgeGroupCount : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/age-groups", async(AnalyticsDbContext context, CancellationToken cancellationToken) =>
            {
                var ageGroups = await context.AgeGroupCounts
                    .FromSqlRaw(Queries.AgeGroupCount)
                    .ToListAsync(cancellationToken);

                if (ageGroups.Count == 0)
                {
                    return Results.Ok(Result.Failure<List<AgeGroupDto>>(Error.NullValue));
                }

                return Results.Ok(Result.Success(ageGroups));

            })
                .Produces<Result<List<AgeGroupDto>>>()
                .WithTags("Analysis");
        }
    }
}
