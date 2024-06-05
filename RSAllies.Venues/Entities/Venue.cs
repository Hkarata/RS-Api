using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Venues.Entities
{
    [Table("Venues", Schema = "Venues")]
    internal class Venue
    {
        // TODO : overprice venues that have more traffic
        public Guid Id { get; init; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Address { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<Session>? Sessions { get; set; }

        // Safe Delete Implementation
        public bool IsDeleted { get; set; }

        public Guid RegionId { get; init; }
        public Guid DistrictId { get; init; }

        // Navigation Properties
        public Region? Region { get; set; }
        public District? District { get; set; }
    }
}
