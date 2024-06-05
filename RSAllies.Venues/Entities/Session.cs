using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Venues.Entities
{
    [Table("Sessions", Schema = "Venues")]
    internal class Session
    {
        public Guid Id { get; init; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Capacity { get; set; }

        // Safe Delete Implementation
        public bool IsDeleted { get; set; }

        public Guid VenueId { get; init; }

        // Navigation Properties
        public Venue? Venue { get; set; }
        public List<Booking>? Bookings { get; set; }
    }
}
