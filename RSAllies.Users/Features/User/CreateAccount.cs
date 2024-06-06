using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Notifications;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Data;

namespace RSAllies.Users.Features.User;

internal abstract class CreateAccount
{
    internal class Command : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal sealed class Handler(UsersDbContext context, IMediator mediator) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var account = new Entities.UserAccount
            {
                Id = request.UserId,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Accounts.Add(account);

            await context.SaveChangesAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Email))
            {
                await mediator.Publish(new AccountMade(request.UserId, request.Email), cancellationToken);
            }

            return Result.Success();
        }
    }
}

public class CreateAccountEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/account", async (CreateAccountDto account, ISender sender) =>
        {
            var request = account.Adapt<CreateAccount.Command>();
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        });
    }
}