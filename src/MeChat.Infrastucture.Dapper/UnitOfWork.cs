using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;

namespace MeChat.Infrastucture.Dapper;
public class UnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get;}
    public UnitOfWork(IUserRepository users)
    {
        Users = users;
    }
}
