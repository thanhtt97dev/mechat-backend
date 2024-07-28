using MeChat.Domain.Abstractions.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MeChat.Persistence.Services.Helpers;

namespace MeChat.Persistence.Interceptors;
public class AuditTableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync
        (DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
         DbContext? dbContext = eventData.Context;
        if(dbContext == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        IEnumerable<EntityEntry<IAuditTable>> entries = dbContext.ChangeTracker.Entries<IAuditTable>();
        if(entries.Count() == 0) return base.SavingChangesAsync(eventData, result, cancellationToken);

        bool isUserTracking = true;
        var userId = UserTrackingAditTableHelper.UserId;
        if (userId == null) isUserTracking = false;

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.Property(nameof(IAuditTable.DeleteAt)).CurrentValue = DateTimeOffset.Now;
                entityEntry.Property(nameof(IAuditTable.IsDeleted)).CurrentValue = true;
                entityEntry.State = EntityState.Modified;
            }
            if(entityEntry.State == EntityState.Modified)
            {
                if (isUserTracking)
                    entityEntry.Property(nameof(IAuditTable.ModifiedBy)).CurrentValue = userId;
                entityEntry.Property(nameof(IAuditTable.ModifiledDate)).CurrentValue = DateTimeOffset.Now;
            }
            if (entityEntry.State == EntityState.Added)
            {
                if (isUserTracking)
                    entityEntry.Property(nameof(IAuditTable.CreatedBy)).CurrentValue = userId;
                entityEntry.Property(nameof(IAuditTable.CreatedDate)).CurrentValue = DateTimeOffset.Now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
