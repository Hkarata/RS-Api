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
    internal abstract class GetAdmins
    {
        internal class Query : IRequest<Result<List<Contracts.Responses.Admin>>>
        {

        }

        internal sealed class Handler(UsersDbContext context) : IRequestHandler<Query, Result<List<Contracts.Responses.Admin>>>
        {

            public async Task<Result<List<Contracts.Responses.Admin>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var admins = await context.Administrators
                    .AsNoTracking()
                    .Include(a => a.Role)
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
                    .OrderBy(a => a.FirstName)
                    .ToListAsync(cancellationToken);

                if (admins.Count == 0)
                {
                    return Result.Failure<List<Contracts.Responses.Admin>>(
                        new Error("GetAdmins.None", "There are no admins")
                        );
                }

                return admins;
            }
        }
    }
}


public class GetAdminsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admins", async (ISender sender) =>
        {
            var request = new GetAdmins.Query();
            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.Ok(result.Error);
        })
            .Produces<Result<List<Admin>>>()
            .WithTags("Admin");
    }
}