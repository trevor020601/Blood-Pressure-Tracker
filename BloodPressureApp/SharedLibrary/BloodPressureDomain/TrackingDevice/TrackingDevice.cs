namespace SharedLibrary.BloodPressureDomain.TrackingDevice;

// Questionable if I even want this domain...

public class TrackingDevice
{
    internal TrackingDevice(TrackingDeviceId id,
                            string manufacturer, 
                            string model,
                            string serialNumber,
                            ConnectionType connectionType,
                            DateTime lastSyncDate)
    {
        Id = id;
        Manufacturer = manufacturer;
        Model = model;
        SerialNumber = serialNumber;
        ConnectionType = connectionType;
        LastSyncDate = lastSyncDate;
    }

    public TrackingDeviceId Id { get; private set; } = default!;

    public string Manufacturer { get; private set; } = default!;

    public string Model { get; private set; } = default!;

    public string SerialNumber { get; private set; } = default!;

    public ConnectionType ConnectionType { get; private set; }

    public DateTime LastSyncDate { get; private set; }
}

public enum ConnectionType
{
    Bluetooth,
    Wifi,
    Manual
    // Other connections?
}
