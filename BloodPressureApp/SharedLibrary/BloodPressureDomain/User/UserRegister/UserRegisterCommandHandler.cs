using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User.UserRegister;

internal sealed class UserRegisterCommandHandler(IUserRepository userRepository) : ICommandHandler<UserRegisterCommand, UserId>
{
    public async Task<Result<UserId>> HandleAsync(UserRegisterCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.CreateAsync(command.Email, command.Password, cancellationToken);
    }
}
