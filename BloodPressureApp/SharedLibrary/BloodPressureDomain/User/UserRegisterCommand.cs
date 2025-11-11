using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.Messaging;

namespace SharedLibrary.BloodPressureDomain.User;

public sealed record UserRegisterCommand(Email Email, string Password) : ICommand<UserId>;
