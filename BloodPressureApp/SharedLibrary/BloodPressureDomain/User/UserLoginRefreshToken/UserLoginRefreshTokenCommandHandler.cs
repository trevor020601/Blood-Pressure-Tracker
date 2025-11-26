using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User.UserLoginRefreshToken;

internal class UserLoginRefreshTokenCommandHandler(IUserRepository userRepository) : ICommandHandler<UserLoginRefreshTokenCommand, UserLoginRefreshTokenResponse>
{
    public async Task<Result<UserLoginRefreshTokenResponse>> HandleAsync(UserLoginRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.LoginRefreshTokenAsync(command.RefreshToken, cancellationToken);
    }
}
