using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hui.Api.Dal.Repositories;

namespace Hui.Api.Bll.Service.Base
{
    /// <summary>
    /// 常用CURD封装
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TRepository, TEntity> : IBaseService<TEntity>
        where TRepository : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly TRepository Repository;

        public BaseService(TRepository repository)
        {
            Repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var retEntity = await Repository.AddAsync(entity);
            await Repository.SaveAsync();
            return retEntity;
        }

        public async Task<int> AddRangeAsync(List<TEntity> entityList)
        {
            Repository.AddRange(entityList);
            return await Repository.SaveAsync();
        }

        public async Task<int> RemoveAsync(object id)
        {
            Repository.Remove(id);
            return await Repository.SaveAsync();
        }

        public async Task<int> RemoveAsync(TEntity entity)
        {
            Repository.Remove(entity);
            return await Repository.SaveAsync();
        }

        public async Task<int> RemoveRangeAsync(List<TEntity> entityList)
        {
            Repository.RemoveRange(entityList);
            return await Repository.SaveAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Repository.Update(entity);
            await Repository.SaveAsync();
            return entity;
        }

        public async Task<int> UpdateRangeAsync(List<TEntity> entityList)
        {
            Repository.UpdateRange(entityList);
            return await Repository.SaveAsync();
        }

        public async Task<TEntity> GetAsync(object id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<T> QueryAsync<T>(Func<IQueryable<TEntity>, T> expression)
        {
            return await Repository.QueryAsync(expression);
        }

        public async Task<T> QueryAsNoTrackingAsync<T>(Func<IQueryable<TEntity>, T> expression)
        {
            return await Repository.QueryAsNoTrackingAsync(expression);
        }
    }
}
