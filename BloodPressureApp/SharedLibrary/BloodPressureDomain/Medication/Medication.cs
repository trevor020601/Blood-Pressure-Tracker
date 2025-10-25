namespace SharedLibrary.BloodPressureDomain.Medication;

public class Medication
{
    internal Medication(MedicationId id,
                        string name, 
                        string schedule,
                        Dosage dosage,
                        DateTime startDate,
                        DateTime? endDate) { 
        Id = id;
        Name = name;
        Schedule = schedule;
        Dosage = dosage;
        StartDate = startDate;
        EndDate = endDate;
    }

    public MedicationId Id { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string Schedule { get; private set; } = default!;

    public Dosage Dosage { get; private set; } = default!;

    public DateTime StartDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    // More properties?
}
