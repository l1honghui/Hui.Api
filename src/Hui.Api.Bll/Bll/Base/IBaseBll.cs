using Hui.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hui.Api.Bll
{ 
    public interface IBaseBll<TEntity> : IDependency
       where TEntity : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(List<TEntity> entityList);

        /// <summary>
        /// 删除（根据主键）
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(object primaryKey);


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(TEntity entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<int> RemoveRangeAsync(List<TEntity> entityList);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(List<TEntity> entityList);

        /// <summary>
        /// 查询（根据主键）
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
       Task<TEntity> GetAsync(object id);

        /// <summary>
        /// 查询（自定义查询表达式）
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> QueryAsync<T>(Func<IQueryable<TEntity>, T> expression);

        /// <summary>
        /// 查询（自定义查询表达式）
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> QueryAsNoTrackingAsync<T>(Func<IQueryable<TEntity>, T> expression);
    }
}
