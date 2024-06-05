using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities;

[Table("Nationalities", Schema = "Users")]
internal class Nationality
{
    public Guid Id { get; set; }
    public string Nation { get; set; } = string.Empty;
    public List<User>? Users { get; set; }
}