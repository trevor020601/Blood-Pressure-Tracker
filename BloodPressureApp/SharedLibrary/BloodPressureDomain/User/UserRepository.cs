using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.BloodPressureDomain.User.UserLogin;
using SharedLibrary.BloodPressureDomain.User.UserRegister;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.DataAccess;
using SharedLibrary.PasswordHasher;
using SharedLibrary.Result;
using SharedLibrary.UnitOfWork;

namespace SharedLibrary.BloodPressureDomain.User;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    Task<Result<UserId>> CreateAsync(Email email, string password, CancellationToken cancellationToken);
    Task<Result<UserId>> RetrieveAsync(Email email, string password, CancellationToken cancellationToken);
    Task<Result.Result> DeleteAsync(Email email, CancellationToken cancellationToken);
}

public sealed class UserRepository(IApplicationDbContext context,
                                   IUnitOfWork unitOfWork,
                                   IPasswordHasher passwordHasher) : IUserRepository
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

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Result.Success(user.Id);
    }

    public async Task<Result<UserId>> RetrieveAsync(Email email,
                                                    string password,
                                                    CancellationToken cancellationToken)
    {
        var user = await GetByEmailAsync(email.Value, cancellationToken);
        if (user is null)
        {
            return Result.Result.Failure<UserId>(UserErrors.UserDoesNotExist);
        }

        var verified = passwordHasher.Verify(password, user.Password);
        if (!verified)
        {
            return Result.Result.Failure<UserId>(UserErrors.IncorrectPassword);
        }

        user.Raise(new UserLoginDomainEvent(user.Id));

        // Might need to decouple the domain events dispatcher from unit of work...
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Result.Success(user.Id);
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

        return Result.Result.Success();
    }

    // Update email

    // Update password

    // Maybe update account status but only under admin privileges...
}