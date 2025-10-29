using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;

namespace SharedLibrary.UnitOfWork;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
