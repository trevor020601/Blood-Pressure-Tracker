using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.DataAccess;
using SharedLibrary.Events;

namespace SharedLibrary.UnitOfWork;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

internal sealed class UnitOfWork(IApplicationDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork 
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await dbContext.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = 
            dbContext.ChangeTracker.Entries<Entity>()
                         .Select(entry => entry.Entity)
                         .SelectMany(entity =>
                         {
                            var domainEvents = entity.DomainEvents;
                            entity.ClearDomainEvents();
                            return domainEvents;
                         }).ToList();

        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}
