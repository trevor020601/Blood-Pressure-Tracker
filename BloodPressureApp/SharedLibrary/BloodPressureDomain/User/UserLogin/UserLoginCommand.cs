using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.Messaging;

namespace SharedLibrary.BloodPressureDomain.User.UserLogin;

public sealed record UserLoginCommand(Email Email, string Password) : ICommand<UserId>;
