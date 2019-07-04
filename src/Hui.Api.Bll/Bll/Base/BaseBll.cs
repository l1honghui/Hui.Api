using Hui.Api.Dal.Repositories;
using Hui.Api.Model.Entity.IEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hui.Api.Bll
{
    /// <summary>
    /// 常用CURD封装
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class BaseBll<TRepository, TEntity, TPrimaryKey> : IBaseBll<TEntity, TPrimaryKey>
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected readonly TRepository Repository;

        public BaseBll(TRepository repository)
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

        public async Task<int> RemoveAsync(TPrimaryKey id)
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

        public async Task<TEntity> GetAsync(TPrimaryKey id)
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