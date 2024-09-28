using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Persistence.Services.Helpers;
using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.InteropServices;

namespace MeChat.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    protected IDbContextTransaction? dbTransaction { get; private set; }

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

    public async Task BeginTransactionAsync([Optional] CancellationToken cancellationToken)
    {
        if (dbTransaction == null)
            return;
        dbTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
        await DisposeAsync();
    }

    public async Task CommitTransactionAsync([Optional] CancellationToken cancellationToken)
    {
        if (dbTransaction == null)
            return;
        await dbTransaction.CommitAsync(cancellationToken);
        await DisposeAsync();
    }

    public async Task RollbackTransactionAsync([Optional] CancellationToken cancellationToken)
    {
        if(dbTransaction == null)
            return;
        await dbTransaction.RollbackAsync(cancellationToken);
        await DisposeAsync();
    }
}
