using SharedLibrary.BloodPressureDomain.Medication;
using SharedLibrary.BloodPressureDomain.Patient;
using SharedLibrary.Extensions;

namespace SharedLibrary.BloodPressureDomain.HealthInformation;

public class HealthInformation
{
    private readonly HashSet<Medication.Medication> _medications = [];

    private HealthInformation() { }

    public HealthInformationId Id { get; private set; } = default!;

    public PatientId PatientId { get; private set; } = default!;

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public string? MiddleName { get; private set; }

    public Sex Gender { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    // I don't think this should be in the domain...
    private int _age;
    public int Age
    {
        get { return _age; }
        private set
        {
            _age = DateTimeExtensions.CalculateAge(new DateTimeRange(DateOfBirth, DateTime.Now));
        }
    }

    // Other properties?
    // BMI?
    // Blood type?

    public static HealthInformation Create(Patient.Patient patient,
                                           string firstName,
                                           string lastName,
                                           string? middleName,
                                           Sex gender, 
                                           DateTime dateOfBirth)
    {
        return new HealthInformation
        {
            Id = new HealthInformationId(Guid.CreateVersion7()),
            PatientId = patient.Id,
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            Gender = gender,
            DateOfBirth = dateOfBirth
        };
    }

    public void AddMedication(string name,
                              string schedule,
                              Dosage dosage,
                              DateTime startDate,
                              DateTime? endDate)
    {
        _medications.Add(new Medication.Medication(
            new MedicationId(Guid.CreateVersion7()),
            name,
            schedule,
            dosage,
            startDate,
            endDate)
        );
    }
}

public enum Sex
{
    Male,
    Female
}