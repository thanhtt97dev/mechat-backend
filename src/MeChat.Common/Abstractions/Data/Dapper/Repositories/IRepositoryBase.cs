using MeChat.Common.Enumerations;

namespace MeChat.Common.Abstractions.Data.Dapper.Repositories;
public interface IRepositoryBase<T> where T : class
{
    Task<T?> FindByIdAsync(Guid id);
    Task<List<T>?> GetManyAsync(string? SearchTerm, IDictionary<string, SortOrderSql> SortColumnWithOrders, int PageIndex, int PageSize);
    Task<int> GetTotalRecord();
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(Guid id);
    string GetSortProperty(string sortProperty);
}
