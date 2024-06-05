using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities
{
    [Table("Roles", Schema = "Users")]
    internal class Role
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
    }
}
