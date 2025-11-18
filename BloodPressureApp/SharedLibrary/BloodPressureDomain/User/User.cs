using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.Events;
using SharedLibrary.Primitives;

namespace SharedLibrary.BloodPressureDomain.User;

public class User : Entity, IAuditableEntity
{
    public User () { }

    public UserId Id { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    // Stored as a hash
    public string Password { get; private set; } = default!;

    //public string Name { get; private set; } = default!;

    public AccountStatus Status { get; private set; }

    // Not sure if I want to do this yet, but create an object for account preferences (like notifications, measurement units, etc.)
    //public AccountPreferences Preferences { get; private set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public static User Create(Email email,
                              string password)
    {
        return new User {
            Id = new UserId(Guid.CreateVersion7()),
            Email = email,
            Password = password,
            Status = AccountStatus.Active 
        };
    }
}

public enum AccountStatus
{
    Active,
    Inactive,
    Deleted
    // Add more statuses?
}