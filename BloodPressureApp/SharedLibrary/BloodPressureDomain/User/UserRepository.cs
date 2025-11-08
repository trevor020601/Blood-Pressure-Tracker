using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.DataAccess;
using SharedLibrary.Result;
using SharedLibrary.UnitOfWork;
using System.Diagnostics;

namespace SharedLibrary.BloodPressureDomain.User;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<bool> Exists(string email, CancellationToken cancellationToken);
    Task<Result.Result> Create(Email email, string password, CancellationToken cancellationToken);
    Task<Result.Result> Delete(Email email, CancellationToken cancellationToken);
}

public sealed class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IApplicationDbContext context,
                          IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> GetByIdAsync(UserId id,
                                          CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email,
                                        CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<bool> Exists(string email,
                                   CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<Result.Result> Create(Email email,
                             string password,
                             CancellationToken cancellationToken)
    {
        var doesUserExist = await Exists(email.Value, cancellationToken);
        if (doesUserExist)
        {
            return UserErrors.ExistingUser;
        }

        var user = User.Create(email, password);

        await _context.Users.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Result.Success();
    }

    public async Task<Result.Result> Delete(Email email,
                                            CancellationToken cancellationToken)
    {
        var doesUserExist = await Exists(email.Value, cancellationToken);
        if (!doesUserExist)
        {
            // Should this be a custom Error?
            return Result.Result.Success();
        }

        await _context.Users.Where(u => u.Email == email)
                            .ExecuteDeleteAsync(cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Result.Success();
    }

    // Update email

    // Update password

    // Maybe update account status but only under admin privileges...
}

public static class UserErrors
{
    public static readonly Error ExistingUser = new("Users.Create", "User with email already exists.", new StackTrace().ToString());
}