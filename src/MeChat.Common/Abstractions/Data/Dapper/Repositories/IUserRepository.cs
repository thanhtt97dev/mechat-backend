using MeChat.Domain.Entities;

namespace MeChat.Common.Abstractions.Data.Dapper.Repositories;
public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByUsernameAndPassword(string username, string password);
    Task<User?> GetUserByEmail(string email);
}
