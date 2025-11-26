using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.BloodPressureDomain.User.UserRevokeRefreshTokens;

internal sealed class UserRevokeRefreshTokensCommandHandler(IUserRepository userRepository) : ICommandHandler<UserRevokeRefreshTokensCommand, bool>
{
    public async Task<Result<bool>> HandleAsync(UserRevokeRefreshTokensCommand command, CancellationToken cancellationToken)
    {
        return await userRepository.RevokeRefreshTokensAsync(command.UserId, cancellationToken);
    }
}
