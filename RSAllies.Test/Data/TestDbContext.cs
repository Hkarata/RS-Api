using Microsoft.EntityFrameworkCore;
using RSAllies.Test.Entities;

namespace RSAllies.Test.Data
{
    internal class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
    {
        internal DbSet<Question> Questions { get; set; }
        internal DbSet<Choice> Choices { get; set; }
        internal DbSet<Answer> Answers { get; set; }
        internal DbSet<Response> Responses { get; set; }
        internal DbSet<Score> Scores { get; set; }
        internal DbSet<SelectedChoice> SelectedChoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
