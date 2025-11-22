using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.Authentication.Policies;

public sealed class UserPolicy
{
    // Use UserId type instead?
    public Guid UserId { get; set; }
    public int PolicyId { get; set; }
    public User User { get; set; } = default!;
    public Policy Policy { get; set; } = default!;
}
