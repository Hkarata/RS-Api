using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities;

[Table("EducationLevels", Schema = "Users")]
internal class EducationLevel
{
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Level { get; set; } = string.Empty;

    public List<User>? Users { get; set; }
}