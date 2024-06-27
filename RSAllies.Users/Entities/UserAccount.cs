using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities;


[Table("Accounts", Schema = "Users")]
internal class UserAccount
{
    public Guid Id { get; init; }

    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    [MaxLength(12)]
    public string Phone { get; set; } = string.Empty;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Safe Delete Implementation
    public bool IsDeleted { get; set; }

    //Navigation Property
    public User? User { get; set; }
}