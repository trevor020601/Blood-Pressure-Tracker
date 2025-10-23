using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;

namespace SharedLibrary.BloodPressureDomain.User;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmail(string email);
    Task<bool> Exists(string email);
}
