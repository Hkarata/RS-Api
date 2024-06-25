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
        public virtual DbSet<RepeatedBookingCountDto> RepeatedBookingCounts { get; set; }
        public virtual DbSet<VenueUtilizationDto> VenueUtilizations { get; set; }
        public virtual DbSet<GenderTestDto> GenderTests { get; set; }
        public virtual DbSet<QuestionAnalysisDto> QuestionAnalyses { get; set; }
        public virtual DbSet<QuestionDifficultyDto> QuestionDifficulties { get; set; }
        public virtual DbSet<ScoresDto> Scores { get; set; }
        public virtual DbSet<TestPassAgeGroupDto> TestPassAgeGroupCounts { get; set; }
        public virtual DbSet<QuestionGenderDto> QuestionGenderCounts { get; set; }
        public virtual DbSet<QuestionAgeGroupDto> QuestionAgeGroups { get; set; }
        public virtual DbSet<TestRetakeDto> TestRetakeCounts { get; set; }
        public virtual DbSet<TestGenderDto> TestGenderCounts { get; set; }
        public virtual DbSet<TestRetakeAgeGroupDto> TestRetakeAgeGroupCounts { get; set; }




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
            modelBuilder.Entity<RepeatedBookingCountDto>().HasNoKey();
            modelBuilder.Entity<VenueUtilizationDto>().HasNoKey();
            modelBuilder.Entity<GenderTestDto>().HasNoKey();
            modelBuilder.Entity<QuestionAnalysisDto>().HasNoKey();
            modelBuilder.Entity<QuestionDifficultyDto>().HasNoKey();
            modelBuilder.Entity<ScoresDto>().HasNoKey();
            modelBuilder.Entity<TestPassAgeGroupDto>().HasNoKey();
            modelBuilder.Entity<QuestionGenderDto>().HasNoKey();
            modelBuilder.Entity<QuestionAgeGroupDto>().HasNoKey();
            modelBuilder.Entity<TestRetakeDto>().HasNoKey();
            modelBuilder.Entity<TestGenderDto>().HasNoKey();
            modelBuilder.Entity<TestRetakeAgeGroupDto>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
