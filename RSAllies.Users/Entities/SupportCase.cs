using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities
{
    [Table("SupportCases", Schema = "Users")]
    internal class SupportCase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(20)]
        public string CaseNo { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
        public bool IsClosed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        // Navigation properties
        public User? User { get; set; }

    }
}
