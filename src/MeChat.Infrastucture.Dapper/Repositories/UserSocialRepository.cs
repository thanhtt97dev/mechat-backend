
using Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace MeChat.Infrastucture.Dapper.Repositories;
public class UserSocialRepository : IUserSocialRepository
{
    private readonly ApplicationDbContext context;

    public UserSocialRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<UserSocial?> GetUserSocial(Guid userId, int socialId, string accountSocialId)
    {
        var query =
@$"SELECT * FROM UserSocial
WHERE UserId = '{userId}' AND SocialId = '{socialId}' AND AccountSocialId = '{accountSocialId}'";

        using SqlConnection connection = context.CreateConnection();
        await connection.OpenAsync();
        var result = await connection.QuerySingleOrDefaultAsync<Domain.Entities.UserSocial>(query);
        await connection.DisposeAsync();

        return result;
    }
}
