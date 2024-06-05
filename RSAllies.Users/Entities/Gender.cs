using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities
{
    [Table("Genders", Schema = "Users")]
    internal class Gender
    {
        public Guid Id { get; init; }

        [MaxLength(7)]
        public string GenderType { get; init; } = string.Empty;

        public List<User>? Users { get; set; }
    }
}
