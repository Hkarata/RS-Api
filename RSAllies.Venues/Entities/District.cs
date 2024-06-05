using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Venues.Entities;

[Table("Districts", Schema = "Venues")]
internal class District
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RegionId { get; set; }
    public Region? Region { get; set; }
}