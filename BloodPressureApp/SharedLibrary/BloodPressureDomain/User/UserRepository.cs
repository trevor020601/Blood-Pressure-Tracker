using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.Authentication;
using SharedLibrary.Authentication.Policies;
using SharedLibrary.Authentication.RefreshToken;
using SharedLibrary.BloodPressureDomain.User.UserLogin;
using SharedLibrary.BloodPressureDomain.User.UserLoginRefreshToken;
using SharedLibrary.BloodPressureDomain.User.UserRegister;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.DataAccess;
using SharedLibrary.Events;
using SharedLibrary.PasswordHasher;
using SharedLibrary.Result;
using SharedLibrary.UnitOfWork;
using System.Security.Claims;

namespace SharedLibrary.BloodPressureDomain.User;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    Task<Result<UserId>> CreateAsync(Email email, string password, CancellationToken cancellationToken);
    Task<Result<UserLoginResponse>> LoginAsync(Email email, string password, CancellationToken cancellationToken);
    Task<Result<UserLoginRefreshTokenResponse>> LoginRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<Result<bool>> RevokeRefreshTokensAsync(UserId userId, CancellationToken cancellationToken);
    Task<Result.Result> DeleteAsync(Email email, CancellationToken cancellationToken);
}

public sealed class UserRepository(IApplicationDbContext context,
                                   IUnitOfWork unitOfWork,
                                   IPasswordHasher passwordHasher,
                                   IJwtProvider jwtProvider, 
                                   IDomainEventsPublisher domainEventsPublisher,
                                   IHttpContextAccessor httpContextAccessor) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email,
                                             CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string email,
                                        CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<Result<UserId>> CreateAsync(Email email,
                                                  string password,
                                                  CancellationToken cancellationToken)
    {
        var doesUserExist = await ExistsAsync(email.Value, cancellationToken);
        if (doesUserExist)
        {
            return Result.Result.Failure<UserId>(UserErrors.ExistingUser);
        }

        var user = User.Create(email, passwordHasher.Hash(password));

        user.Raise(new UserRegisterDomainEvent(user.Id));

        await context.Users.AddAsync(user, cancellationToken);

        await context.UserPolicies.AddAsync(new UserPolicy { 
            UserId = user.Id.Value,
            PolicyId = Policy.UserId
        }, 
        cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await domainEventsPublisher.PublishDomainEventsAsync();

        return Result.Result.Success(user.Id);
    }

    public async Task<Result<UserLoginResponse>> LoginAsync(Email email,
                                                 string password,
                                                 CancellationToken cancellationToken)
    {
        var user = await GetByEmailAsync(email.Value, cancellationToken);
        if (user is null)
        {
            return Result.Result.Failure<UserLoginResponse>(UserErrors.UserDoesNotExist);
        }

        var verified = passwordHasher.Verify(password, user.Password);
        if (!verified)
        {
            return Result.Result.Failure<UserLoginResponse>(UserErrors.IncorrectPassword);
        }

        var token = await jwtProvider.Generate(user);

        var refreshToken = new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = user.Id.Value,
            Token = jwtProvider.GenerateRefreshToken(),
            ExpiresOn = DateTime.Now.AddDays(7)
        };

        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        user.Raise(new UserLoginDomainEvent(user.Id));

        await domainEventsPublisher.PublishDomainEventsAsync();

        return Result.Result.Success(new UserLoginResponse(token, refreshToken.Token));
    }

    public async Task<Result<UserLoginRefreshTokenResponse>> LoginRefreshTokenAsync(string refreshToken, 
                                                                                    CancellationToken cancellationToken)
    {
        var token = await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken, cancellationToken);

        if (token is null || token.ExpiresOn == DateTime.Now)
        {
            return Result.Result.Failure<UserLoginRefreshTokenResponse>(UserErrors.ExpiredRefreshToken);
        }

        var accessToken = await jwtProvider.Generate(token.User);

        token.Token = jwtProvider.GenerateRefreshToken();
        token.ExpiresOn = DateTime.Now.AddDays(7);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        token.User.Raise(new UserLoginDomainEvent(token.User.Id));

        await domainEventsPublisher.PublishDomainEventsAsync();

        return Result.Result.Success(new UserLoginRefreshTokenResponse(accessToken, token.Token));
    }

    public async Task<Result<bool>> RevokeRefreshTokensAsync(UserId userId, CancellationToken cancellationToken)
    {
        Guid? contextUserId = Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid parsed) ? 
            parsed : 
            null;

        if (userId.Value != contextUserId)
        {
            return Result.Result.Failure<bool>(UserErrors.CannotRevokeRefreshTokens);
        }

        await context.RefreshTokens
            .Where(r => r.UserId == userId.Value)
            .ExecuteDeleteAsync(cancellationToken);

        // Domain Event?

        return Result.Result.Success(true);
    }

    public async Task<Result.Result> DeleteAsync(Email email,
                                                 CancellationToken cancellationToken)
    {
        var user = await GetByEmailAsync(email.Value, cancellationToken);
        if (user is null)
        {
            return Result.Result.Failure(UserErrors.UserDoesNotExist);
        }

        context.Users.Remove(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // TODO: Create UserDeleteEvent

        return Result.Result.Success();
    }

    // Update email

    // Update password

    // Maybe update account status but only under admin privileges...
}