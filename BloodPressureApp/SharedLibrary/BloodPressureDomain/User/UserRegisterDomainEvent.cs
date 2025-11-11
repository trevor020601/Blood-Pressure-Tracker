using SharedLibrary.Events;

namespace SharedLibrary.BloodPressureDomain.User;

public sealed record UserRegisterDomainEvent(UserId UserId) : IDomainEvent;
