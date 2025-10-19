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

    public Task<User?> GetByIdAsync(UserId id)
    {
        return _context.Users
            .SingleOrDefaultAsync(u => u.Id == id);
    }
}
