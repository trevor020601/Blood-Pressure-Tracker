using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.DataAccess;
using SharedLibrary.UnitOfWork;

namespace SharedLibrary.BloodPressureDomain.User;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<bool> Exists(string email, CancellationToken cancellationToken);
    Task Create(Email email, string password, CancellationToken cancellationToken);
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

    public async Task Create(Email email,
                             string password,
                             CancellationToken cancellationToken)
    {
        var doesUserExist = await Exists(email.Value, cancellationToken);
        if (doesUserExist)
        {
            throw new InvalidOperationException("User with email already exists.");
        }

        var user = User.Create(email, password);

        await _context.Users.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Maybe return a result?
    }

    public async Task Delete(Email email,
                             CancellationToken cancellationToken)
    {
        var doesUserExist = await Exists(email.Value, cancellationToken);
        if (!doesUserExist)
        {
            return;
        }

        await _context.Users.Where(u => u.Email == email)
                            .ExecuteDeleteAsync(cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Maybe return a result?
    }

    // Update email

    // Update password

    // Maybe update account status but only under admin privileges...
}
