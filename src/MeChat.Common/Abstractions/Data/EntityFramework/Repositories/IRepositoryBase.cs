﻿namespace MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveMultiple(List<TEntity> entities);
}
