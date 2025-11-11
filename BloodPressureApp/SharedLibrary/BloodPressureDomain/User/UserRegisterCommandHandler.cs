using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User;

internal sealed class UserRegisterCommandHandler(IUserRepository userRepository) : ICommandHandler<UserRegisterCommand, UserId>
{
    public async Task<Result<UserId>> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.CreateAsync(command.Email, command.Password, cancellationToken);
    }
}
