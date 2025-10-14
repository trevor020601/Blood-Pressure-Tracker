namespace SharedLibrary.BloodPressureDomain.HealthInformation;

public class HealthInformation
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string Name { get; private set; } = default!;

    public Sex Gender { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    // Possibly add a property for age

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