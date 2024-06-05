using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Responses;
using RSAllies.Users.Data;

namespace RSAllies.Users.Features.Support;

internal abstract class GetCases
{
    internal class Query : IRequest<Result<List<CaseDto>>>
    {

    }

    internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<List<CaseDto>>>
    {
        public async Task<Result<List<CaseDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cases = await context.SupportCases
                .AsNoTracking()
                .Where(c => !c.IsClosed)
                .Select(c => new CaseDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Username = c.Username,
                    CaseNo = c.CaseNo,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt
                }).ToListAsync(cancellationToken);

            if (cases.Count == 0)
            {
                return Result.Failure<List<CaseDto>>(new Error("GetCases.None",
                    "There are no support cases"));
            }

            return cases;
        }
    }
}

public class GetCasesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cases", async (ISender sender) =>
        {
            var request = new GetCases.Query();
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
        .Produces<Result<List<CaseDto>>>()
        .WithTags("Support");
    }
}