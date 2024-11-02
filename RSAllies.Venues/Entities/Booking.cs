using System.ComponentModel.DataAnnotations.Schema;
using RSAllies.Shared.DataTypes;

namespace RSAllies.Venues.Entities
{
    [Table("Bookings", Schema = "Venues")]
    internal class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }

        // TODO : create  an event such that when a booking is created, the status is set to Booked
        // and when the booking is paid, the status is set to paid
        // and when the booking is cancelled, the status is set to cancelled
        // the time interval between the booking and the payment should be 24 hours

        // TODO : 6 hours before the session starts, the status should be set to confirmed
        // otherwise, the status should be set to cancelled, and session capacity should be updated
        public BookingStatus Status { get; set; }
        public DateTime BookedAt { get; init; }
        public DateTime? UpdatedAt { get; set; }

        // Soft Delete Implementation
        public bool IsDeleted { get; set; }

        // Navigation Property
        public Session? Session { get; set; }
    }
}
