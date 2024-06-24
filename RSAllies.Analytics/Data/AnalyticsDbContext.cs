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
        public virtual DbSet<PeakBookingDaysDto> PeakBookingDays { get; set; }
        public virtual DbSet<PeakBookingMonthDto> PeakBookingMonths { get; set; }
        public virtual DbSet<PeakBookingYearDto> PeakBookingYears { get; set; }
        public virtual DbSet<MostPopularVenueDto> MostPopularVenues { get; set; }
        public virtual DbSet<LeastPopularVenueDto> LeastPopularVenues { get; set; }
        public virtual DbSet<CancellationRateDto> CancellationRates { get; set; }
        public virtual DbSet<ConfirmationRateDto> ConfirmationRates { get; set; }
        public virtual DbSet<BookingStatusCountDto> BookingStatusCounts { get; set; }
        public virtual DbSet<VenueBookingStatusCount> VenueBookingStatusCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderDto>().HasNoKey();
            modelBuilder.Entity<AgeGroupDto>().HasNoKey();
            modelBuilder.Entity<LicenseDto>().HasNoKey();
            modelBuilder.Entity<EducationLevelDto>().HasNoKey();
            modelBuilder.Entity<AverageSessionDto>().HasNoKey();
            modelBuilder.Entity<BookingRateDto>().HasNoKey();
            modelBuilder.Entity<PeakBookingTimesDto>().HasNoKey();
            modelBuilder.Entity<PeakBookingDaysDto>().HasNoKey();
            modelBuilder.Entity<PeakBookingMonthDto>().HasNoKey();
            modelBuilder.Entity<PeakBookingYearDto>().HasNoKey();
            modelBuilder.Entity<MostPopularVenueDto>().HasNoKey();
            modelBuilder.Entity<LeastPopularVenueDto>().HasNoKey();
            modelBuilder.Entity<CancellationRateDto>().HasNoKey();
            modelBuilder.Entity<ConfirmationRateDto>().HasNoKey();
            modelBuilder.Entity<BookingStatusCountDto>().HasNoKey();
            modelBuilder.Entity<VenueBookingStatusCount>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
