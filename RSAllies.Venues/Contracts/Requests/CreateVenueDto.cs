using RSAllies.Shared.Extensions;

namespace RSAllies.Venues.Contracts.Requests
{
    public record struct CreateVenueDto(
        string Name,
        string Address,
        [SystemData] Guid DistrictId,
        [SystemData] Guid RegionId,
        int Capacity
        );
}
