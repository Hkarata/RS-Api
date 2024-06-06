using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Checks;

namespace RSAllies.Users.Features.Checks
{
    internal abstract class NamesCheck
    {
        internal class Query : IRequest<Result<bool>>
        {
            internal string FirstName { get; set; } = string.Empty;
            internal string MiddleName { get; set; } = string.Empty;
            internal string LastName { get; set; } = string.Empty;
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<bool>>
        {
            public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await context.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.FirstName == request.FirstName && u.MiddleName == request.MiddleName && u.LastName == request.LastName, cancellationToken);

                return result;
            }
        }
    }
}


public class NamesCheckEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/checks/check-names", async (Names names, ISender sender) =>
        {
            var request = new NamesCheck.Query
            {
                FirstName = names.FirstName,
                MiddleName = names.MiddleName,
                LastName = names.LastName
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<bool>>()
            .WithTags("Checks");
    }
}