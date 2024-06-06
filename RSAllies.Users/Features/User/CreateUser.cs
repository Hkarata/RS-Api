using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Requests;
using RSAllies.Users.Data;

namespace RSAllies.Users.Features.User;

internal abstract class CreateUser
{
    internal class Command : IRequest<Result>
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsForeigner { get; set; }
        public Guid GenderId { get; set; }
        public Guid EducationLevelId { get; set; }
        public Guid LicenseClassId { get; set; }
    }

    internal sealed class Handler(UsersDbContext context) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = new Entities.User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Age = DateTime.Today.Year - request.DateOfBirth.Year,
                Address = request.Address,
                Identification = request.Identification,
                IsForeigner = request.IsForeigner,
                GenderId = request.GenderId,
                LicenseClassId = request.LicenseClassId,
                EducationLevelId = request.EducationLevelId,
            };

            context.Users.Add(user);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}


public class CreateUserEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/user", async (CreateUserDto user, ISender sender) =>
        {
            var request = user.Adapt<CreateUser.Command>();

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("User");
    }
}