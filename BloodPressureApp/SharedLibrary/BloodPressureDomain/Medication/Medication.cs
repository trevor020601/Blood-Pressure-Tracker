namespace SharedLibrary.BloodPressureDomain.Medication;

public class Medication
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string Name { get; private set; } = default!;

    public string Schedule { get; private set; } = default!;

    public Dosage Dosage { get; private set; } = default!;

    public DateTime StartDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    // More properties?
}
