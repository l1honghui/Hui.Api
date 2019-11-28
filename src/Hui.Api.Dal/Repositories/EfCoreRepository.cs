using Hui.Api.Common.EmrException;
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
    /// 封装常用CURD的仓储方法类
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EfCoreRepository<TDbContext, TEntity> : RepositoryBase<TEntity>
       where TDbContext : DbContext
        where TEntity : class
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        protected TDbContext Context;

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        protected virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public EfCoreRepository(TDbContext dbContext)
        {
            Context = dbContext;
        }

        /// <summary>
        /// 找不到则抛EntityNotFoundException异常，如果需要自定义异常信息，用FirstOrDefault查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity Get(object id)
        {
            var val = Table.Find(id);
            if (val == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            return val;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (propertySelectors != null)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Add(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public override Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(Add(entity));
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }

        public override void Remove(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entity)
        {
            Table.AddRange(entity);
        }

        public override void UpdateRange(IEnumerable<TEntity> entitys)
        {
            Table.UpdateRange(entitys);
        }

        public override void RemoveRange(IEnumerable<TEntity> entity)
        {
            Table.RemoveRange(entity);
        }

        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public DbContext GetDbContext()
        {
            return Context;
        }

        public override int Save()
        {
            return Context.SaveChanges();
        }

        public override async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public override void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Context.Database.CurrentTransaction == null)
            {
                Context.Database.BeginTransaction(isolationLevel);
            }
        }

        public override void Commit()
        {
            var transaction = Context.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public override void Rollback()
        {
            Context.Database.CurrentTransaction?.Rollback();
        }
    }
}
