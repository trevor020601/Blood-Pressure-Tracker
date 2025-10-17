using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;

namespace SharedLibrary.PasswordHasher;

[InjectDependency(ServiceLifetime.Singleton)]
public interface IPasswordHasher
{
    string Hash(string password);
}
