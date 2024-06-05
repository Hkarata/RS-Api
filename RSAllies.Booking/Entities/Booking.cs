using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.Booking.Entities
{
    [Table("Bookings", Schema = "Booking")]
    internal class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public DateTime BookedAt { get; set; }
    }
}
