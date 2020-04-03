using System;
using System.Collections.Generic;

namespace library_api.Interfaces
{
    public interface IBaseApi<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity Get(int id);
        List<TEntity> List();
        TEntity Update(TEntity entity);
        int Delete(TEntity entity);
    }
}