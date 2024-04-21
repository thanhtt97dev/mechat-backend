using MeChat.Domain.Abstractions.Dapper.Repositories;

namespace MeChat.Domain.Abstractions.Dapper;
public interface IUnitOfWork
{
    IRepositoryBase<Domain.Entities.User> Users { get; }
}
