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

internal abstract class GetCase
{
    internal class Query : IRequest<Result<CaseDto>>
    {
        internal Guid CaseId { get; set; }
    }

    internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<CaseDto>>
    {
        public async Task<Result<CaseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var caseQuery = await context.SupportCases
                .AsNoTracking()
                .Where(c => c.Id == request.CaseId && !c.IsClosed)
                .Select(c => new CaseDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Username = c.Username,
                    CaseNo = c.CaseNo,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (caseQuery is null)
            {
                return Result.Failure<CaseDto>(new Error("GetCase.NonExistentCase", "" +
                    "The specified case does not exist"));
            }

            return caseQuery;
        }
    }
}

public class GetCaseEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/support/{id:guid}", async (Guid id, ISender sender) =>
        {
            var request = new GetCase.Query { CaseId = id };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
        .Produces<Result<CaseDto>>()
        .WithTags("Support");
    }
}