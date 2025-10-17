using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.BloodPressureDomain.Medication;

public class Medication
{
    public MedicationId Id { get; private set; } = default!;

    public UserId UserId { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string Schedule { get; private set; } = default!;

    public Dosage Dosage { get; private set; } = default!;

    public DateTime StartDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    // More properties?
}
