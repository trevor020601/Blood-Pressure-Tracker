using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.Authentication.Policies;

public sealed class UserPolicy
{
    public UserId UserId { get; set; } = default!;
    public int PolicyId { get; set; }
    public User User { get; set; } = default!;
    public Policy Policy { get; set; } = default!;
}
