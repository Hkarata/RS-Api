using Microsoft.EntityFrameworkCore;

namespace RSAllies.Jobs.Data
{
    public class JobsDbContext : DbContext
    {
        public JobsDbContext(DbContextOptions<JobsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
