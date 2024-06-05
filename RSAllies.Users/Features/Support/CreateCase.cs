using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Shared.Requests;
using RSAllies.Users.Data;
using RSAllies.Users.Entities;
using RSAllies.Users.Features.Support;

namespace RSAllies.Users.Features.Support
{
    internal abstract class CreateCase
    {

        internal class Command : IRequest<Result>
        {
            public Guid UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var supportCase = new SupportCase
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Username = request.Username,

                    // TODO create  a stateful component that will manage case numbers
                    CaseNo = Guid.NewGuid().ToString(),
                    Message = request.Message,
                    CreatedAt = DateTime.UtcNow
                };

                context.SupportCases.Add(supportCase);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateCaseEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/support", async (CreateCaseDto caseDto, ISender sender) =>
        {
            var request = new CreateCase.Command
            {
                UserId = caseDto.UserId,
                Username = caseDto.Username,
                Message = caseDto.Message
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
        .Produces<Result>()
        .WithTags("Support");
    }
}