namespace SharedLibrary.BloodPressureDomain.User;

public class User
{
    public UserId Id { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    // Should this be a hashed string?
    public HashCode Password { get; private set; }

    //public DateTime CreatedDate { get; private set; }

    public AccountStatus Status { get; private set; }

    // Not sure if I want to do this yet, but create an object for account preferences (like notifications, measurement units, etc.)
    //public AccountPreferences Preferences { get; private set; }
}

public enum AccountStatus
{
    Active,
    Inactive,
    Deleted
    // Add more statuses?
}