using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.Authentication.Policies;

public sealed class AdminPolicy
{
    // Use UserId type instead?
    public Guid AdminId { get; set; }
    public int PolicyId { get; set; }
    public User Admin { get; set; } = default!; 
    public Policy Policy { get; set; } = default!;
}
