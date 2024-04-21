using MeChat.Domain.Abstractions.Dapper.Repositories;

namespace MeChat.Domain.Abstractions.Dapper;
public interface IUnitOfWork
{
    IUserRepository Users { get; }
}
