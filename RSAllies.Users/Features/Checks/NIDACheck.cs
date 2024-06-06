using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Checks;

namespace RSAllies.Users.Features.Checks
{
    internal abstract class NIDACheck
    {
        internal class Query : IRequest<Result<bool>>
        {
            internal string NIDA { get; set; } = string.Empty;
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<bool>>
        {
            public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await context.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Identification == request.NIDA, cancellationToken);

                return result;
            }
        }
    }
}
public class NIDACheckEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/checks/check-nida", async (string nida, ISender sender) =>
        {
            var request = new NIDACheck.Query
            {
                NIDA = nida
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<bool>>()
            .WithTags("Checks");
    }
}

