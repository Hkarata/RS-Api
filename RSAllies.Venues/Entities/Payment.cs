using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Venues.Entities
{
    [Table("Payments", Schema = "Payments")]
    internal class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PaymentNumber { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }

    internal enum PaymentStatus
    {
        Pending,
        Paid
    }
}
