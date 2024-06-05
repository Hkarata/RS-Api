using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Venues.Entities
{
    [Table("Regions", Schema = "Venues")]
    internal class Region
    {
        public Guid Id { get; init; }

        [MaxLength(50)]
        public string Name { get; init; } = string.Empty;
        public List<Venue>? Venues { get; set; }
        public List<District>? Districts { get; set; }
    }
}
