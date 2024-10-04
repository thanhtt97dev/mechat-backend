using Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Common.Shared.Constants;
using MeChat.Domain.Entities;
using MeChat.Infrastucture.Dapper;
using Microsoft.Data.SqlClient;

namespace MeChat.Infrastucture.Data.Dapper.Repositories;
public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext context;

    public NotificationRepository(ApplicationDbContext context)
    {
        this.context = context;
    }


    #region GetManyAsync
    public async Task<IEnumerable<Notification>> GetManyAsync(Guid id, int pageIndex, int pageSize)
    {
        if (pageIndex <= 0)
            pageIndex = AppConstants.Page.IndexDefault;

        if (pageSize > AppConstants.Page.SizeMaximun)
            pageSize = AppConstants.Page.SizeMaximun;

        var totalRecord = await GetTotalRecord(id);
        var totalPage = totalRecord / pageSize;

        if (totalRecord % pageSize != 0 || totalRecord == 0)
            totalPage += 1;

        if (pageIndex > totalPage)
            pageIndex = totalPage;

        var query =
                @$"SELECT * 
                FROM [Notification]
                WHERE UserId = '{id}'
                ORDER BY CreatedDate DESC
                OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QueryAsync<Domain.Entities.Notification>(query);

        return result.ToList();
    }
    #endregion

    #region GetTotalRecord
    public async Task<int> GetTotalRecord(Guid id)
    {
        var query =
            $@"SELECT COUNT(*)
            FROM [Notification]
            WHERE UserId = '{id}'";

        using SqlConnection connection = context.GetConnection();
        await connection.OpenAsync();

        var result = await connection.QueryFirstAsync<int>(query);

        return result;
    }
    #endregion

}
