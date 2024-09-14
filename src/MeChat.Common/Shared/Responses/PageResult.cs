using MeChat.Common.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace MeChat.Common.Shared.Response;
public class PageResult<TData>
{
    public List<TData>? Items { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public int Total { get; }
    public bool HasNextPage => PageIndex * PageSize < Total;
    public bool HasPreviousPage => PageIndex > 1;
    public PageResult(List<TData>? items, int pageIndex, int pageSize, int total)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Total = total;
    }

    public static async Task<PageResult<TData>> CreateAsync(IQueryable<TData> query , int pageIndex = AppConstants.Page.IndexDefault, int pageSize = AppConstants.Page.SizeDefault)
    {
        if(pageIndex <= 0)
            pageIndex = AppConstants.Page.IndexDefault;

        if(pageSize > AppConstants.Page.SizeMaximun)
            pageSize = AppConstants.Page.SizeMaximun;

        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new(items, pageIndex, pageSize, totalCount);
    }

    public static PageResult<TData> Create(List<TData>? items, int pageIndex, int pageSize, int totalCount)
    {
        return new(items, pageIndex, pageSize, totalCount);
    }
        
}
