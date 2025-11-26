using SharedLibrary.Events;

namespace SharedLibrary.BloodPressureDomain.User.UserLoginRefreshToken;

public sealed record UserLoginRefreshTokenDomainEvent(UserId UserId) : IDomainEvent;
