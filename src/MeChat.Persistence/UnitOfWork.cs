using MeChat.Domain.Abstractions.EntityFramework;

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
        await context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}
