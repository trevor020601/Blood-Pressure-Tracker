using SharedLibrary.Events;

namespace SharedLibrary.BloodPressureDomain.User.UserLogin;

public sealed record UserLoginDomainEvent(UserId UserId) : IDomainEvent;
