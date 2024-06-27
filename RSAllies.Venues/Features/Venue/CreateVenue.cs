using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Requests;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Venue;
using RSAllies.Venues.Services;

namespace RSAllies.Venues.Features.Venue
{
    internal abstract class CreateVenue
    {
        internal class Command : IRequest<Result>
        {
            public string Name { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public int Capacity { get; set; }
            public Guid DistrictId { get; set; }
            public Guid RegionId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!await DatabaseService.IsVenueUniqueAsync(context, request.Name))
                {
                    return new Result(isSuccess: false, new Error("CreateVenue.ExistentVenue",
                        "The specified venue already exists"));
                }

                var venue = new Entities.Venue
                {
                    Name = request.Name,
                    Address = request.Address,
                    ImageUrl = request.ImageUrl,
                    Capacity = request.Capacity,
                    DistrictId = request.DistrictId,
                    RegionId = request.RegionId,
                    IsDeleted = false
                };

                context.Venues.Add(venue);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateVenueEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/venue", async (CreateVenueDto venue, ISender sender) =>
        {
            var request = new CreateVenue.Command
            {
                Name = venue.Name,
                Address = venue.Address,
                ImageUrl = venue.ImageUrl,
                Capacity = venue.Capacity,
                DistrictId = venue.DistrictId,
                RegionId = venue.RegionId,
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Venue");
    }
}
