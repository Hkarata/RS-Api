using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities
{
    [Table("Administrators", Schema = "Users")]
    internal class Administrator
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }

        // Navigation Property
        public Role? Role { get; set; }
    }
}
