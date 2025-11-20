using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User.UserLogin;

internal sealed class UserLoginCommandHandler(IUserRepository userRepository) : ICommandHandler<UserLoginCommand, string>
{
    public async Task<Result<string>> HandleAsync(UserLoginCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.LoginAsync(command.Email, command.Password, cancellationToken);
    }
}
