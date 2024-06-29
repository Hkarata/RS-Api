using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Contracts.Responses;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Venue;

namespace RSAllies.Venues.Features.Venue
{
    internal abstract class GetVenue
    {
        internal class Query : IRequest<Result<VenueDto>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Query, Result<VenueDto>>
        {
            public async Task<Result<VenueDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var venue = await context.Venues
                    .AsNoTracking()
                    .Where(v => v.Id == request.Id && !v.IsDeleted)
                    .Include(v => v.District)
                    .Include(v => v.Region)
                    .Include(v => v.Sessions)
                    .Select(v => new VenueDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Address = v.Address,
                        ImageUrl = v.ImageUrl,
                        Capacity = v.Capacity,
                        District = v.District!.Name,
                        Region = v.Region!.Name,
                        Sessions = v.Sessions!
                            .Where(s => !s.IsDeleted)
                            .Select(s => new SessionDto
                            {
                                Id = s.Id,
                                Date = s.Date,
                                StartTime = s.StartTime,
                                EndTime = s.EndTime,
                                Capacity = s.Capacity,
                            }).ToList()
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (venue is null)
                {
                    return Result.Failure<VenueDto>(new Error("GetVenue.NonExistentVenue",
                        "The specified venue does not exist"));
                }

                return venue;
            }
        }
    }
}

public class GetVenueEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/venue/{id:guid}", async (Guid id, ISender sender) =>
        {
            var request = new GetVenue.Query { Id = id };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<VenueDto>>()
            .WithTags("Venue");
    }
}
