using SharedLibrary.BloodPressureDomain.TrackingDevice;

namespace SharedLibrary.BloodPressureDomain.BloodPressureReading;

public class BloodPressureReading
{
    internal BloodPressureReading(BloodPressureReadingId id,
                                  TrackingDeviceId trackingDeviceId,
                                  int systolic,
                                  int diastolic,
                                  int heartRate,
                                  double weight,
                                  DateTime readingDate,
                                  string? note,
                                  Source measurementSource,
                                  Position? position)
    {
        Id = id;
        TrackingDeviceId = trackingDeviceId;
        Systolic = systolic;
        Diastolic = diastolic;
        HeartRate = heartRate;
        Weight = weight;
        ReadingDate = readingDate;
        Note = note;
        MeasurementSource = measurementSource;
        Position = position;
    }

    public BloodPressureReadingId Id { get; private set; } = default!;

    public TrackingDeviceId TrackingDeviceId { get; private set; } = default!;

    public int Systolic { get; private set; }

    public int Diastolic { get; private set; }

    public int HeartRate { get; private set; }

    public double Weight { get; private set; }

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