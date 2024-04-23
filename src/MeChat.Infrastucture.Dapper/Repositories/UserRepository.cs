using Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Common.Constants;
using MeChat.Common.Enumerations;
using MeChat.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace MeChat.Infrastucture.Dapper.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;

    public UserRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    #region Add async
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
    #endregion

    #region Delete async
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
    #endregion

    #region Find by Id async
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
    #endregion

    #region Get many async
    public async Task<List<User>?> GetManyAsync(string? searchTerm, IDictionary<string, Common.Enumerations.SortOrderSql> sortColumnWithOrders, int pageIndex = Page.IndexDefault, int pageSize = Page.SizeDefault)
    {
        if (pageIndex <= 0)
            pageIndex = Page.IndexDefault;

        if (pageSize > Page.SizeMaximun)
            pageSize = Page.SizeMaximun;

        var query =
@$"SELECT * FROM [User]
WHERE (1 = 1) AND
{nameof(User.Id)} LIKE '%{searchTerm}%' OR
{nameof(User.Username)} LIKE '%{searchTerm}%'
ORDER BY ";
        if (sortColumnWithOrders.Count == 0)
        {
            query += @$"{GetSortProperty(string.Empty)}";
        }
        else
        {
            foreach (var item in sortColumnWithOrders)
            {
                var x = GetSortProperty(item.Key);
                var order = item.Value == SortOrderSql.Descending ?
                    $"{GetSortProperty(item.Key)} DESC, " :
                    $"{GetSortProperty(item.Key)} ASC, ";
                query += order;
            }
            query = query.Remove(query.Length - 2);
        }

        query += $"\nOFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";


        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QueryAsync<User>(query);

        await connection.DisposeAsync();
        return result.ToList();
    }
    #endregion

    #region Get sort property
    public string GetSortProperty(string sortProperty)
    => sortProperty.ToLower() switch
    {
        "id" => nameof(User.Id),
        "password" => nameof(User.Username),
        _ => nameof(User.Id)
    };
    #endregion

    #region Get total record
    public async Task<int> GetTotalRecord()
    {
        var sql =
@"SELECT COUNT(*) 
FROM [dbo].[User]";
        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QueryFirstAsync<int>(sql);

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Get user by username and password
    public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
    {
        var query =
@"SELECT [Id]
      ,[Username]
      ,[Password]
  FROM [dbo].[User]
WHERE [Username] = @Username AND [Password] = @Password";

        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username, Password = password});

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Update
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
    #endregion

}
