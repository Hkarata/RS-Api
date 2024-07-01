using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Support;

namespace RSAllies.Users.Features.Support
{
    internal class CloseCase
    {
        internal class Command : IRequest<Result>
        {
            public Guid CaseId { get; set; }
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var supportCase = await context.SupportCases
                        .Where(c => c.Id == request.CaseId && !c.IsClosed)
                        .SingleOrDefaultAsync(cancellationToken);

                if (supportCase is null)
                {
                    return Result.Failure(Error.NullValue);
                }

                supportCase.IsClosed = true;

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CloseCaseEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/support-case/{caseId:guid}", async (Guid caseId, ISender sender) =>
        {
            var request = new CloseCase.Command { CaseId = caseId };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        });
    }
}