using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.Authentication.RefreshToken;

public class RefreshToken
{
    public RefreshTokenId Id { get; set; } = default!;
    public string Token { get; set; } = default!;
    public UserId UserId { get; set; } = default!;
    public DateTime ExpiresOn { get; set; }

    public User User { get; set; } = default!;
}
