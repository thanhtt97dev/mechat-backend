namespace MeChat.Common.Abstractions.Data.EntityFramework;
public interface IUnitOfWork : IAsyncDisposable
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
}
