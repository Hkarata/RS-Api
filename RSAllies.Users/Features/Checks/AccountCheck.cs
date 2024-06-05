using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Contracts.Responses;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Checks;

namespace RSAllies.Users.Features.Checks
{
    internal abstract class AccountCheck
    {
        internal class Query : IRequest<Result<AccountCheckResult>>
        {
            public string PhoneNumber { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<AccountCheckResult>>
        {
            public async Task<Result<AccountCheckResult>> Handle(Query request, CancellationToken cancellationToken)
            {
                var phoneNumberTask = context.Accounts.AnyAsync(a => a.Phone == request.PhoneNumber, cancellationToken);
                var emailTask = context.Accounts.AnyAsync(a => a.Email == request.Email, cancellationToken);

                await Task.WhenAll(phoneNumberTask, emailTask);

                var phoneNumberExists = phoneNumberTask.Result;
                var emailExists = emailTask.Result;

                var result = new AccountCheckResult { EmailExists = emailExists, PhoneNumberExists = phoneNumberExists };

                return result;

            }
        }
    }
}

public class AccountCheckEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/checks/check-account", async (AccountDto account, ISender sender) =>
        {
            var request = new AccountCheck.Query { PhoneNumber = account.PhoneNumber, Email = account.Email };
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .Produces<Result<AccountCheckResult>>()
            .WithTags("Checks");
    }
}
