namespace SharedLibrary.BloodPressureDomain.BloodPressureReading;

public class BloodPressureReading
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public int Systolic { get; private set; }

    public int Diastolic { get; private set; }

    public int HeartRate { get; private set; }

    public DateTime ReadingDate { get; private set; }

    public string? Note { get; private set; }

    public Source MeasurementSource { get; private set; }

    public Position? Position { get; private set; }
}

public enum Source
{
    Manual,
    SmartCuff,
    WearableDevice
    // More sources?
}

public enum Position
{
    Sitting,
    Standing,
    Lying
    // More positions?
}
