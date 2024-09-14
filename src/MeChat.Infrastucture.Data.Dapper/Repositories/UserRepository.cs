using Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Enumerations;
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
    public async Task<int> AddAsync(Domain.Entities.User entity)
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

        using SqlConnection connection = context.GetConnection();
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
        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(sql, new { Id = id });

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Find by Id async
    public async Task<Domain.Entities.User?> FindByIdAsync(Guid id)
    {
        var sql =
@"SELECT * 
FROM [dbo].[User]
WHERE Id = @Id";
        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleOrDefaultAsync<Domain.Entities.User>(sql, new { Id = id });

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Get many async
    public async Task<List<Domain.Entities.User>?> GetManyAsync
        (string? searchTerm, IDictionary<string, SortOrderSql> sortColumnWithOrders,
        int pageIndex = AppConstants.Page.IndexDefault, int pageSize = AppConstants.Page.SizeDefault)
    {
        if (pageIndex <= 0)
            pageIndex = AppConstants.Page.IndexDefault;

        if (pageSize > AppConstants.Page.SizeMaximun)
            pageSize = AppConstants.Page.SizeMaximun;

        var query =
@$"SELECT * FROM [User]
WHERE (1 = 1) AND
{nameof(Domain.Entities.User.Id)} LIKE '%{searchTerm}%' OR
{nameof(Domain.Entities.User.Username)} LIKE '%{searchTerm}%'
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


        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QueryAsync<Domain.Entities.User>(query);

        await connection.DisposeAsync();
        return result.ToList();
    }
    #endregion

    #region Get sort property
    public string GetSortProperty(string sortProperty)
    => sortProperty.ToLower() switch
    {
        "id" => nameof(Domain.Entities.User.Id),
        "password" => nameof(Domain.Entities.User.Username),
        _ => nameof(Domain.Entities.User.Id)
    };
    #endregion

    #region Get total record
    public async Task<int> GetTotalRecord()
    {
        var sql =
@"SELECT COUNT(*) 
FROM [dbo].[User]";
        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QueryFirstAsync<int>(sql);

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Get user by username and password
    public async Task<Domain.Entities.User?> GetUserByUsernameAndPassword(string username, string password)
    {
        var query =
@"SELECT *
  FROM [dbo].[User]
WHERE [Username] = @Username AND [Password] = @Password";

        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleOrDefaultAsync<Domain.Entities.User>(query, new { Username = username, Password = password});

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Get user by email
    public async Task<Domain.Entities.User?> GetUserByEmail(string email)
    {
        var sql =
@"SELECT *
FROM [dbo].[User]
WHERE [Email] = @Email";
        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleOrDefaultAsync<Domain.Entities.User>(sql, new { Email = email });

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Update
    public async Task<int> UpdateAsync(Domain.Entities.User entity)
    {
        var sql =
@"UPDATE [dbo].[User]
   SET [Id] = @Id
      ,[Username] = @Username
      ,[Password] = @Password
 WHERE [Id] = @Id";
        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(sql, entity);

        await connection.DisposeAsync();
        return result;
    }
    #endregion

    #region Get user by accout socialId
    public async Task<User?> GetUserByAccountSocial(string accountSocialId, int socialId)
    {
        var sql =
@$"SELECT * 
FROM [User]
JOIN UserSocial ON [User].Id = UserSocial.UserId
WHERE UserSocial.AccountSocialId = '{accountSocialId}' AND UserSocial.SocialId = '{socialId}'";
        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();
        var result = await connection.QuerySingleOrDefaultAsync<Domain.Entities.User>(sql);
        await connection.DisposeAsync();
        return result;
    }
    #endregion
}
