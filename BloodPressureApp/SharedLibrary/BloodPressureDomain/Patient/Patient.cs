using SharedLibrary.BloodPressureDomain.BloodPressureReading;
using SharedLibrary.BloodPressureDomain.HealthInformation;
using SharedLibrary.BloodPressureDomain.TrackingDevice;
using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.BloodPressureDomain.Patient;

public class Patient
{
    private readonly HashSet<BloodPressureReading.BloodPressureReading> _bloodPressureReadings = [];

    private readonly HashSet<TrackingDevice.TrackingDevice> _trackingDevices = [];

    private Patient() { }

    public PatientId Id { get; private set; } = default!;

    public UserId UserId { get; private set; } = default!;

    public HealthInformationId HealthInformationId { get; private set; } = default!;

    public IReadOnlyList<BloodPressureReading.BloodPressureReading> BloodPressureReadings => [.. _bloodPressureReadings];

    public IReadOnlyList<TrackingDevice.TrackingDevice> TrackingDevices => [.. _trackingDevices];

    public static Patient Create(User.User user, 
                                 HealthInformation.HealthInformation healthInformation)
    {
        return new Patient
        {
            Id = new PatientId(Guid.CreateVersion7()),
            UserId = user.Id,
            HealthInformationId = healthInformation.Id
        };
    }

    public void AddBloodPressureReading(TrackingDevice.TrackingDevice trackingDevice,
                                        int systolic,
                                        int diastolic,
                                        int heartRate,
                                        double weight,
                                        DateTime readingDate,
                                        string? note,
                                        Source measurementSource,
                                        Position? measurementPosition)
    {
        _bloodPressureReadings.Add(new BloodPressureReading.BloodPressureReading(
            new BloodPressureReadingId(Guid.CreateVersion7()),
            trackingDevice.Id,
            systolic,
            diastolic,
            heartRate,
            weight,
            readingDate,
            note,
            measurementSource,
            measurementPosition)
        );
    }

    public void AddTrackingDevice(string manufacturer,
                                  string model,
                                  string serialNumber,
                                  ConnectionType connectionType,
                                  DateTime lastSyncDate)
    {
        _trackingDevices.Add(new TrackingDevice.TrackingDevice(
            new TrackingDeviceId(Guid.CreateVersion7()),
            manufacturer,
            model,
            serialNumber,
            connectionType,
            lastSyncDate)
        );
    }
}
