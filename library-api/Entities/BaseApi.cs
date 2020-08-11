using System.Collections.Generic;
using System.Linq;
using library_api.Interfaces;

namespace library_api.Services {
    public class BaseApi<TEntity> : IBaseApi<TEntity> where TEntity : class
    {
        protected readonly MyDbContext _myDbContext;

        public BaseApi(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public TEntity Create(TEntity entity)
        {
            _myDbContext.Set<TEntity>().Add(entity);
            _myDbContext.SaveChanges();
            _myDbContext.Entry(entity).Reload();
            return entity;
        }

        public int Delete(TEntity entity)
        {
            _myDbContext.Remove(entity);
            return _myDbContext.SaveChanges();
        }

        public List<TEntity> List()
        {
            return _myDbContext.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return _myDbContext.Set<TEntity>().Find(id);
        }

        public TEntity Update(TEntity entity)
        {
            _myDbContext.Set<TEntity>().Update(entity);
            _myDbContext.SaveChanges();
            _myDbContext.Entry(entity).Reload();
            return entity;
        }

        public void Dispose()
        {
            _myDbContext.Dispose();
        }
    }
}