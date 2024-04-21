namespace MeChat.Domain.Abstractions.EntityFramework;
public interface IUnitOfWork : IAsyncDisposable
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
}
