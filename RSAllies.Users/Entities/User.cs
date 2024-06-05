using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Users.Entities;

[Table("Users", Schema = "Users")]
internal class User
{
    public Guid Id { get; set; }
    [MaxLength(20)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string MiddleName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Identification { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime DateOfBirth { get; init; }

    // Nationality Classifier
    public bool IsForeigner { get; set; }

    // Foreign Keys
    public Guid GenderId { get; set; }
    public Guid EducationLevelId { get; set; }
    public Guid LicenseClassId { get; set; }


    // Navigation Property
    public UserAccount? Account { get; set; }
    public Gender? Gender { get; set; }
    public EducationLevel? EducationLevel { get; set; }
    public LicenseClass? LicenseClass { get; set; }
}