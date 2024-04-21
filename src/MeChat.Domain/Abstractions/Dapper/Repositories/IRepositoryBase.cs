using System.Collections.Generic;

namespace MeChat.Domain.Abstractions.Dapper.Repositories;
public interface IRepositoryBase<T> where T : class
{
    Task<T?> FindByIdAsync (Guid id);
    Task<IReadOnlyList<T>?> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(Guid id);
}
