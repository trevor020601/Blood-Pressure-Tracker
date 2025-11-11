using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.DataAccess;
using SharedLibrary.PasswordHasher;
using SharedLibrary.Result;
using SharedLibrary.UnitOfWork;

namespace SharedLibrary.BloodPressureDomain.User;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    Task<Result<UserId>> CreateAsync(Email email, string password, CancellationToken cancellationToken);
    Task<Result.Result> DeleteAsync(Email email, CancellationToken cancellationToken);
}

public sealed class UserRepository(IApplicationDbContext context,
                                   IUnitOfWork unitOfWork,
                                   IPasswordHasher passwordHasher) : IUserRepository
{
    public async Task<User?> GetByIdAsync(UserId id,
                                          CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

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

    public async Task<Result.Result> DeleteAsync(Email email,
                                            CancellationToken cancellationToken)
    {
        var doesUserExist = await ExistsAsync(email.Value, cancellationToken);
        if (!doesUserExist)
        {
            // Should this be a custom Error?
            return Result.Result.Success();
        }

        await context.Users.Where(u => u.Email == email)
                            .ExecuteDeleteAsync(cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Result.Success();
    }

    // Update email

    // Update password

    // Maybe update account status but only under admin privileges...
}