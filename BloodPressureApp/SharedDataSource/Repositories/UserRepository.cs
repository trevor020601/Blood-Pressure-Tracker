using Microsoft.EntityFrameworkCore;
using SharedLibrary.BloodPressureDomain.User;

namespace SharedDataSource.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(UserId id)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email.Value == email);
    }

    public async Task<bool> Exists(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email.Value == email);
    }
}
