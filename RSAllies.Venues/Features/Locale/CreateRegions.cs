using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Entities;
using RSAllies.Venues.Features.Locale;

namespace RSAllies.Venues.Features.Locale
{
    internal abstract class CreateRegions
    {
        internal class Command : IRequest<Result>
        {
            public List<Data> Data { get; set; } = new();
        }

        public class Data
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var regions = request.Data.Select(r => new Region { Id = r.Id, Name = r.Name }).ToList();

                context.Regions.AddRange(regions!);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateRegionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/regions", async (List<CreateRegionDto> regions, ISender sender) =>
        {
            var list = regions.Select(r => new CreateRegions.Data { Id = r.Id, Name = r.region }).ToList();
            var request = new CreateRegions.Command { Data = list };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Locale"); ;
    }
}
