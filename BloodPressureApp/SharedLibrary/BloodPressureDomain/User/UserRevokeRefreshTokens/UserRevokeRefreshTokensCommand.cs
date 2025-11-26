using SharedLibrary.Messaging;

namespace SharedLibrary.BloodPressureDomain.User.UserRevokeRefreshTokens;

public sealed record UserRevokeRefreshTokensCommand(UserId UserId) : ICommand<bool>;
