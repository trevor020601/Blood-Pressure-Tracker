using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.Authentication.RefreshToken;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = default!;
    // User UserId?
    public Guid UserId { get; set; }
    public DateTime ExpiresOn { get; set; }

    public User User { get; set; } = default!;
}
