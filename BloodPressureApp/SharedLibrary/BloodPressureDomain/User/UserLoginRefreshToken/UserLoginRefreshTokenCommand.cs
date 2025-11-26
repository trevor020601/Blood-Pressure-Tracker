using SharedLibrary.Messaging;

namespace SharedLibrary.BloodPressureDomain.User.UserLoginRefreshToken;

public sealed record UserLoginRefreshTokenCommand(string RefreshToken) : ICommand<UserLoginRefreshTokenResponse>;
