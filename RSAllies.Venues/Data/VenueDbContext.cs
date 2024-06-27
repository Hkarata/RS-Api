using Microsoft.EntityFrameworkCore;
using RSAllies.Venues.Entities;

namespace RSAllies.Venues.Data
{
    internal class VenueDbContext(DbContextOptions<VenueDbContext> options) : DbContext(options)
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
