using Dapper;
using MeChat.Domain.Abstractions.Dapper.Repositories;
using MeChat.Domain.Entities;
using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MeChat.Infrastucture.Dapper.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;

    public UserRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<int> AddAsync(User entity)
    {
        var sql =
@"INSERT INTO [dbo].[User]
           ([Id]
           ,[Username]
           ,[Password])
     VALUES
           (@Id
           ,@Username>
           ,@Password)";

        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(sql, entity);

        await connection.DisposeAsync();
        return result;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var sql =
@"DELETE FROM [dbo].[User]
      WHERE Id = @Id";
        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(sql, new { Id = id });

        await connection.DisposeAsync();
        return result;
    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        var sql =
@"SELECT [Id]
      ,[Username]
      ,[Password]
FROM [dbo].[User]
WHERE Id = @Id";
        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });

        await connection.DisposeAsync();
        return result;
    }

    public async Task<IReadOnlyList<User>?> GetAllAsync()
    {
        var sql =
@"SELECT [Id]
      ,[Username]
      ,[Password]
FROM [dbo].[User]";
        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QueryAsync<User>(sql);

        await connection.DisposeAsync();
        return result.ToList();
    }

    public async Task<int> UpdateAsync(User entity)
    {
        var sql =
@"UPDATE [dbo].[User]
   SET [Id] = @Id
      ,[Username] = @Username
      ,[Password] = @Password
 WHERE [Id] = @Id";
        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(sql, entity);

        await connection.DisposeAsync();
        return result;
    }
}
