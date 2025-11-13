using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User.UserLogin;

internal sealed class UserLoginCommandHandler(IUserRepository userRepository) : ICommandHandler<UserLoginCommand, UserId>
{
    public async Task<Result<UserId>> HandleAsync(UserLoginCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.RetrieveAsync(command.Email, command.Password, cancellationToken);
    }
}
