using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Users.Contracts.Responses;
using RSAllies.Users.Data;
using RSAllies.Users.Features.Admin;

namespace RSAllies.Users.Features.Admin
{
    internal abstract class GetAdmin
    {
        internal class Query : IRequest<Result<Contracts.Responses.Admin>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<Contracts.Responses.Admin>>
        {
            public async Task<Result<Contracts.Responses.Admin>> Handle(Query request, CancellationToken cancellationToken)
            {
                var admin = await context.Administrators
                    .AsNoTracking()
                    .Where(a => a.Id == request.Id)
                    .Select(a => new Contracts.Responses.Admin
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Username = a.Username,
                        Phone = a.Phone,
                        Email = a.Email,
                        IsActive = a.IsActive,
                        Password = a.Password,
                        Role = a.Role!.Name
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (admin is null)
                {
                    return Result.Failure<Contracts.Responses.Admin>(
                        new Error("GetAdmin.NotFound", "The specified admin does not exist")
                        );
                }

                return admin;
            }
        }
    }
}

public class GetAdminEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/{id}", async (Guid id, ISender sender) =>
        {
            var request = new GetAdmin.Query
            {
                Id = id
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.Ok(result.Error);
        })
            .Produces<Result<Admin>>()
            .WithTags("Admin");
    }
}