using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedLibrary.Primitives;

namespace SharedLibrary.DataAccess.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData,
                                           result,
                                           cancellationToken);
        }

        var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                entry.Property(a => a.CreatedOn).CurrentValue = DateTime.Now;
            }

            if (entry.State is EntityState.Modified)
            {
                entry.Property(m => m.ModifiedOn).CurrentValue = DateTime.Now;
            }
        }

        return base.SavingChangesAsync(eventData,
                                       result,
                                       cancellationToken);
    }
}
