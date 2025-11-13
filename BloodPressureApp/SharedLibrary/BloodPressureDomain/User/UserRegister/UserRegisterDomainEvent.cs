using SharedLibrary.Events;

namespace SharedLibrary.BloodPressureDomain.User.UserRegister;

public sealed record UserRegisterDomainEvent(UserId UserId) : IDomainEvent;
