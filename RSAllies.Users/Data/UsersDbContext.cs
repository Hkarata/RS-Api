using Microsoft.EntityFrameworkCore;
using RSAllies.Users.Entities;

namespace RSAllies.Users.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    internal DbSet<Administrator> Administrators { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<UserAccount> Accounts { get; set; }
    internal DbSet<Gender> Genders { get; set; }
    internal DbSet<SupportCase> SupportCases { get; set; }
    internal DbSet<EducationLevel> EducationLevels { get; set; }
    internal DbSet<Nationality> Nationalities { get; set; }
    internal DbSet<LicenseClass> LicenseClasses { get; set; }
    internal DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-one relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Account) // User has one UserAccount
            .WithOne(ua => ua.User) // UserAccount has one User
            .HasForeignKey<UserAccount>(ua => ua.Id); // Foreign key property

        // Seeding the Gender table
        modelBuilder.Entity<Gender>().HasData(
            new Gender { Id = Guid.NewGuid(), GenderType = "Male" },
            new Gender { Id = Guid.NewGuid(), GenderType = "Female" }
            );

        // Identification Uniqueness
        modelBuilder.Entity<User>().HasIndex(u => u.Identification)
            .IsUnique(true);

        modelBuilder.Entity<Administrator>().HasIndex(a => a.Username)
            .IsUnique(true);

        // User's First Name, Middle Name and Last Name Should be unique
        modelBuilder.Entity<User>().HasIndex(u => new { u.FirstName, u.MiddleName, u.LastName })
            .IsUnique(true);

        modelBuilder.Entity<Administrator>().HasIndex(a => new { a.FirstName, a.LastName })
            .IsUnique(true);

        // Data Seeding
        modelBuilder.Entity<LicenseClass>().HasData(
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class A" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class A1" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class A2" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class A3" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class B" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class C" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class C1" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class C2" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class C3" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class D" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class E" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class F" },
            new LicenseClass { Id = Guid.NewGuid(), Class = "Class G" }
            );

        modelBuilder.Entity<EducationLevel>().HasData(
            new EducationLevel { Id = Guid.NewGuid(), Level = "Uneducated" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Class 7" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Form 2" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Form 4" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Form 6" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Diploma" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Bachelor's Degree" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "Master's Degree" },
            new EducationLevel { Id = Guid.NewGuid(), Level = "PHD" }
            );

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "SuperUser" },
            new Role { Id = Guid.NewGuid(), Name = "Administrator" },
            new Role { Id = Guid.NewGuid(), Name = "Manager" }
            );

        base.OnModelCreating(modelBuilder);
    }
}