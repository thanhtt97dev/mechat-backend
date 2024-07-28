using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Persistence.Services.Helpers;

namespace MeChat.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangeUserTrackingAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        UserTrackingAditTableHelper.UserId = userId;
        await context.SaveChangesAsync(cancellationToken);
        UserTrackingAditTableHelper.UserId = null;
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}
