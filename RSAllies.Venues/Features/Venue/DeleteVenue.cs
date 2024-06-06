using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Data;
using RSAllies.Venues.Features.Venue;

namespace RSAllies.Venues.Features.Venue
{
    internal abstract class DeleteVenue
    {
        internal class Command : IRequest<Result>
        {
            public Guid VenueId { get; set; }
        }

        internal sealed class Handler(VenueDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var sessions = await context.Sessions
                    .Where(s => s.VenueId == request.VenueId && !s.IsDeleted)
                    .ToListAsync(cancellationToken);

                if (sessions.Count == 0)
                {
                    return Result.Failure(new Error("DeleteVenue.NonExistent", "The specified venue does not exist"));
                }

                sessions.ForEach(s => s.IsDeleted = true);

                // TODO: implement user notification because the venue was deleted hence all sessions were deleted

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class VenueDeleteEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/venue/{venueId:guid}", async (Guid venueId, ISender sender) =>
        {
            var request = new DeleteVenue.Command { VenueId = venueId };
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Venue");
    }
}
