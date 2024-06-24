using Microsoft.EntityFrameworkCore;
using RSAllies.Analytics.Contracts;

namespace RSAllies.Analytics.Data
{
    internal class AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : DbContext(options)
    {
        public virtual DbSet<GenderDto> GenderCounts { get; set; }
        public virtual DbSet<AgeGroupDto> AgeGroupCounts { get; set; }
        public virtual DbSet<LicenseDto> LicenseCounts { get; set; }
        public virtual DbSet<EducationLevelDto> EducationLevelCounts { get; set; }
        public virtual DbSet<AverageSessionDto> AverageSessions { get; set; }
        public virtual DbSet<BookingRateDto> BookingRates { get; set; }
        public virtual DbSet<PeakBookingTimesDto> PeakBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderDto>().HasNoKey();
            modelBuilder.Entity<AgeGroupDto>().HasNoKey();
            modelBuilder.Entity<LicenseDto>().HasNoKey();
            modelBuilder.Entity<EducationLevelDto>().HasNoKey();
            modelBuilder.Entity<AverageSessionDto>().HasNoKey();
            modelBuilder.Entity<BookingRateDto>().HasNoKey();
            modelBuilder.Entity<PeakBookingTimesDto>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
