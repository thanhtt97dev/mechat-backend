using MeChat.Domain.Abstractions.Dapper;
using MeChat.Domain.Abstractions.Dapper.Repositories;
using MeChat.Domain.Entities;

namespace MeChat.Infrastucture.Dapper;
public class UnitOfWork : IUnitOfWork
{
    public IRepositoryBase<User> Users { get;}

    public UnitOfWork(IRepositoryBase<User> users)
    {
        Users = users;
    }
}
