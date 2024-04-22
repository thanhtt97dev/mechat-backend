using MeChat.Common.Abstractions.Data.Dapper.Repositories;

namespace MeChat.Common.Abstractions.Data.Dapper;
public interface IUnitOfWork
{
    public IUserRepository Users { get; }
}
