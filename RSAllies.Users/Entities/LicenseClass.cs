using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities;

[Table("LicenseClasses", Schema = "Users")]
internal class LicenseClass
{
    public Guid Id { get; set; }

    [MaxLength(20)]
    public string Class { get; set; } = string.Empty;

    public List<User>? Users { get; set; }
}