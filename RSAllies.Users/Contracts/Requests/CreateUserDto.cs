using RSAllies.Shared.Extensions;

namespace RSAllies.Users.Contracts.Requests;

public record CreateUserDto
{
    public string FirstName { get; set; } = string.Empty;

    [PersonalIdentifiableInformation]
    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [PersonalIdentifiableInformation]
    public string Identification { get; set; } = string.Empty;

    [PersonalIdentifiableInformation]
    public string Address { get; set; } = string.Empty;

    [SensitiveData]
    public string Password { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public bool IsForeigner { get; set; }

    [SystemData]
    public Guid GenderId { get; set; }

    [SystemData]
    public Guid EducationLevelId { get; set; }

    [SystemData]
    public Guid LicenseClassId { get; set; }
}