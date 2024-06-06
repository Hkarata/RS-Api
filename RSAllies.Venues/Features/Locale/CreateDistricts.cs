using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Entities;

namespace RSAllies.Venues.Features.Locale;

internal abstract class CreateDistricts
{

    internal class Command : IRequest<Result>
    {
        public List<Data>? Data { get; set; }
    }

    internal class Data
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    internal sealed class Handler(VenueDbContext context) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var districts = request.Data?.Select(d => new District
            {
                Id = Guid.NewGuid(),
                Name = d.Name,
                RegionId = d.Id
            })
            .ToList();

            context.Districts.AddRange(districts!);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}

public class CreateDistrictsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/districts", async (List<CreateDistrictDto> districts, ISender sender) =>
        {
            var list = districts.Select(d => new CreateDistricts.Data
            {
                Id = d.Id,
                Name = d.District
            }).ToList();

            var request = new CreateDistricts.Command { Data = list };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Locale");
    }
}