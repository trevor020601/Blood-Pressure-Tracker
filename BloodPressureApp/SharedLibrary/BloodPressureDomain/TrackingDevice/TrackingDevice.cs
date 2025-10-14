namespace SharedLibrary.BloodPressureDomain.TrackingDevice;

// Questionable if I even want this domain...

public class TrackingDevice
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

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
