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
    public class BaseBll <TRepository, TEntity, TPrimaryKey> :  IBaseBll<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
         where TRepository : IRepository<TEntity, TPrimaryKey>
    {
        private readonly TRepository _repository;

        public BaseBll(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var retEntity = await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return retEntity;
        }

        public async Task<int> AddRangeAsync(List<TEntity> entityList)
        {
            _repository.AddRange(entityList);
            return await _repository.SaveAsync();
        }

        public async Task<int> RemoveAsync(TPrimaryKey id)
        {
            _repository.Remove(id);
            return await _repository.SaveAsync();
        }

        public async Task<int> RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);
            return await _repository.SaveAsync();
        }

        public async Task<int> RemoveRangeAsync(List<TEntity> entityList)
        {
            _repository.RemoveRange(entityList);
            return await _repository.SaveAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
            return entity;
        }

        public async Task<int> UpdateRangeAsync(List<TEntity> entityList)
        {
            _repository.UpdateRange(entityList);
            return await _repository.SaveAsync();
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<T> QueryAsync<T>(Func<IQueryable<TEntity>, T> expression)
        {
            return await _repository.QueryAsync(expression);
        }

        public async Task<T> QueryAsNoTrackingAsync<T>(Func<IQueryable<TEntity>, T> expression)
        {
            return await _repository.QueryAsNoTrackingAsync(expression);
        }
    }
}