using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.DataAccess;

namespace SharedLibrary.Events;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IDomainEventsPublisher
{
    Task PublishDomainEventsAsync();
}

internal sealed class DomainEventsPublisher(IApplicationDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher)
{
    public async Task PublishDomainEventsAsync()
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
