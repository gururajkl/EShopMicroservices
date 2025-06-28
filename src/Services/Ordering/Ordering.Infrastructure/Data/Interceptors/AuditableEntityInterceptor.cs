using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedBy = "Gururaj KL";
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if(entry.State is EntityState.Added || entry.State is EntityState.Modified || entry.HasChangedOwnedEntity())
            {
                entry.Entity.LastModifiedBy = "Gururaj KL";
                entry.Entity.LastModifiedAt = DateTime.UtcNow;
            }
        }
    }
}
