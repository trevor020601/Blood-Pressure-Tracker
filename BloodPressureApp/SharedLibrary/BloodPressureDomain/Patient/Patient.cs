using SharedLibrary.BloodPressureDomain.BloodPressureReading;
using SharedLibrary.BloodPressureDomain.HealthInformation;
using SharedLibrary.BloodPressureDomain.TrackingDevice;
using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.Events;
using SharedLibrary.Primitives;

namespace SharedLibrary.BloodPressureDomain.Patient;

public class Patient : Entity, IAuditableEntity
{
    private readonly HashSet<BloodPressureReading.BloodPressureReading> _bloodPressureReadings = [];

    private readonly HashSet<TrackingDevice.TrackingDevice> _trackingDevices = [];

    private Patient() { }

    public PatientId Id { get; private set; } = default!;

    public UserId UserId { get; private set; } = default!;

    public HealthInformationId HealthInformationId { get; private set; } = default!;

    public IReadOnlyList<BloodPressureReading.BloodPressureReading> BloodPressureReadings => [.. _bloodPressureReadings];

    public IReadOnlyList<TrackingDevice.TrackingDevice> TrackingDevices => [.. _trackingDevices];

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

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

    public void RemoveBloodPressureReading(BloodPressureReadingId bloodPressureReadingId)
    {
        var bloodPressureReading = _bloodPressureReadings.FirstOrDefault(b =>  b.Id == bloodPressureReadingId);
        if (bloodPressureReading is null)
        {
            return;
        }
        _bloodPressureReadings.Remove(bloodPressureReading);
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

    public void RemoveTrackingDevice(TrackingDeviceId trackingDeviceId)
    {
        var trackingDevice = _trackingDevices.FirstOrDefault(t => t.Id == trackingDeviceId);
        if (trackingDevice is null)
        {
            return;
        }
        _trackingDevices.Remove(trackingDevice);
    }
}
