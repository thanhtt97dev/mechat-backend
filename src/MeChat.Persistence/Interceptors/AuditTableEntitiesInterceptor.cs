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

        IEnumerable<EntityEntry<IDateTracking>> entriesDateTracking = dbContext.ChangeTracker.Entries<IDateTracking>();
        foreach (var entityEntry in entriesDateTracking)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Property(nameof(IDateTracking.CreatedDate)).CurrentValue = DateTimeOffset.Now;
                    break;
                case EntityState.Modified:
                    entityEntry.Property(nameof(IDateTracking.ModifiledDate)).CurrentValue = DateTimeOffset.Now;
                    break;
                case EntityState.Deleted:
                    entityEntry.Property(nameof(IDateTracking.ModifiledDate)).CurrentValue = DateTimeOffset.Now;
                    break;
            }
        }

        IEnumerable<EntityEntry<ISoftDelete>> entriesSoftDelete = dbContext.ChangeTracker.Entries<ISoftDelete>();
        foreach (var entityEntry in entriesSoftDelete)
        {
            if (entityEntry.State != EntityState.Deleted)
                continue;
            entityEntry.Property(nameof(ISoftDelete.DeleteAt)).CurrentValue = DateTimeOffset.Now;
            entityEntry.Property(nameof(ISoftDelete.IsDeleted)).CurrentValue = true;
            entityEntry.State = EntityState.Modified;
        }

        IEnumerable<EntityEntry<IUserTracking>> entriesUserTracking = dbContext.ChangeTracker.Entries<IUserTracking>();
        if (entriesUserTracking.Any(
             x => x.State == EntityState.Deleted ||
             x.State == EntityState.Modified ||
             x.State == EntityState.Added) && 
             UserTrackingAditTableHelper.UserId == null)
        {
            throw new Exception("Must be save changes data with user tracking");
        }

        foreach (var entityEntry in entriesUserTracking)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Property(nameof(IUserTracking.CreatedBy)).CurrentValue = UserTrackingAditTableHelper.UserId;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entityEntry.Property(nameof(IUserTracking.CreatedBy)).CurrentValue = UserTrackingAditTableHelper.UserId;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
