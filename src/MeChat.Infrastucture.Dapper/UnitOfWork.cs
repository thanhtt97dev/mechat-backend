using MeChat.Domain.Abstractions.Dapper;
using MeChat.Domain.Abstractions.Dapper.Repositories;
using MeChat.Domain.Entities;

namespace MeChat.Infrastucture.Dapper;
public class UnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get;}
    public UnitOfWork(IUserRepository users)
    {
        Users = users;
    }
}
