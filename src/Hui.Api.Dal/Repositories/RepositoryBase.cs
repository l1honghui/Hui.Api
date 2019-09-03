using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hui.Api.Dal.Repositories
{
    /// <summary>
    ///  默认实现部分方法，底层实现交给子类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> 
       where TEntity : class
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAll();
        }

        public abstract TEntity Get(object id);
        
        public Task<TEntity> GetAsync(object id)
        {
            return Task.FromResult(Get(id));
        }

        public virtual void Remove(object id)
        {
            var val = Get(id);
            Remove(val);
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual T QueryAsNoTracking<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll().AsNoTracking());
        }

        public virtual Task<T> QueryAsync<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return Task.FromResult(Query(queryMethod));
        }

        public virtual Task<T> QueryAsNoTrackingAsync<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return Task.FromResult(QueryAsNoTracking(queryMethod));
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public abstract TEntity Add(TEntity entity);

        public virtual Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(Add(entity));
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public abstract void Remove(TEntity entity);

        public virtual Task RemoveAsync(TEntity entity)
        {
            Remove(entity);
            return Task.FromResult(0);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Remove(entity);
            }
        }

        public virtual Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Remove(predicate);
            return Task.FromResult(0);
        }

        public abstract void AddRange(IEnumerable<TEntity> entity);

        public abstract void UpdateRange(IEnumerable<TEntity> entitys);

        public abstract void RemoveRange(IEnumerable<TEntity> entity);

        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        public abstract int Save();

        public abstract Task<int> SaveAsync();

        public abstract void BeginTransaction(IsolationLevel isolationLevel);

        public abstract void Commit();

        public abstract void Rollback();
    }
}
