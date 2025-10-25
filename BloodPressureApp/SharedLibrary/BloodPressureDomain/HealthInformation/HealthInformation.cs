using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.Extensions;

namespace SharedLibrary.BloodPressureDomain.HealthInformation;

public class HealthInformation
{
    public HealthInformationId Id { get; private set; } = default!;

    public UserId UserId { get; private set; } = default!;

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public string? MiddleName { get; private set; }

    public Sex Gender { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    // I don't think this should be in the domain...
    private int _age;
    public int Age {
        get { return _age; }
        set
        {
            _age = DateTimeExtensions.CalculateAge(new DateTimeRange(DateOfBirth, DateTime.Now));
        } 
    }

    public double Weight { get; private set; }

    public DateTime RecordedDate { get; private set; }

    // Other properties?
    // BMI?
    // Blood type?
}

public enum Sex
{
    Male,
    Female
}