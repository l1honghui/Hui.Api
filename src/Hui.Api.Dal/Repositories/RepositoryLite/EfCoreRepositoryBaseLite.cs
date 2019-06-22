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
    /// 不带主键操作的仓储
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public class EfCoreRepositoryBaseLite<TDbContext, TEntity> : RespositoryBaseLite<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public TDbContext Context = null;

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EfCoreRepositoryBaseLite(TDbContext dbContext)
        {
            Context = dbContext;
        }

        public override T QueryAsNoTracking<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(Table.AsNoTracking());
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

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
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

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
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

        public override void InsertRange(params TEntity[] entity)
        {
            Table.AddRange(entity);
        }

        public override void UpdateRange(params TEntity[] entitys)
        {
            // UpdateRange同时具备新增和更新功能
            Table.UpdateRange(entitys);
        }

        public override void DeleteRange(params TEntity[] entity)
        {
            Table.RemoveRange(entity);
        }

        public override int Save()
        {
            return Context.SaveChanges();
        }

        public async override Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public override void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public override void Rollback()
        {
            if (Context.Database.CurrentTransaction != null)
            {
                Context.Database.CurrentTransaction.Rollback();
            }
        }
    }
}