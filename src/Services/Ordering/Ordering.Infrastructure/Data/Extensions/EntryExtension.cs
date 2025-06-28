using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ordering.Infrastructure.Data.Extensions;

public static class EntryExtension
{
    public static bool HasChangedOwnedEntity(this EntityEntry entry)
    {
        return entry.References.Any(r => r.TargetEntry is not null && r.TargetEntry.Metadata.IsOwned() &&
                                   (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
