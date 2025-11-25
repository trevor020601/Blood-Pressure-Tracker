using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User.UserLogin;

internal sealed class UserLoginCommandHandler(IUserRepository userRepository) : ICommandHandler<UserLoginCommand, UserLoginResponse>
{
    public async Task<Result<UserLoginResponse>> HandleAsync(UserLoginCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.LoginAsync(command.Email, command.Password, cancellationToken);
    }
}
