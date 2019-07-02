using Hui.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hui.Api.Dal.Repositories
{
    /// <summary>
    /// Dal接口基类，声明大部分通用CURD的标准方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IRepository<TEntity, TPrimaryKey> :  IDependency
    {
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        TEntity Get(TPrimaryKey id);

        Task<TEntity> GetAsync(TPrimaryKey id);

        TEntity FirstOrDefault(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        void Remove(TPrimaryKey id);

        Task RemoveAsync(TPrimaryKey id);


        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        T QueryAsNoTracking<T>(Func<IQueryable<TEntity>, T> queryMethod);

        Task<T> QueryAsync<T>(Func<IQueryable<TEntity>, T> queryMethod);

        Task<T> QueryAsNoTrackingAsync<T>(Func<IQueryable<TEntity>, T> queryMethod);

        IQueryable<TEntity> GetAll();

        List<TEntity> GetAllList();

        Task<List<TEntity>> GetAllListAsync();

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Remove(TEntity entity);

        Task RemoveAsync(TEntity entity);

        void Remove(Expression<Func<TEntity, bool>> predicate);

        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);

        int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        long LongCount();

        Task<long> LongCountAsync();

        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        void AddRange(IEnumerable<TEntity> entity);

        void UpdateRange(IEnumerable<TEntity> entitys);

        void RemoveRange(IEnumerable<TEntity> entity);

        int Save();

        Task<int> SaveAsync();

        void BeginTransaction(IsolationLevel isolationLevel);

        void Commit();

        void Rollback();

    }
}
