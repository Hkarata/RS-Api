using Microsoft.EntityFrameworkCore;

namespace RSAllies.Analytics.Data
{
    internal class AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
