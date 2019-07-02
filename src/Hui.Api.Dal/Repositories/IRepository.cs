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
    public interface IRepository<TEntity, TPrimaryKey> : IRepositoryLite<TEntity>
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
      
    }
}
