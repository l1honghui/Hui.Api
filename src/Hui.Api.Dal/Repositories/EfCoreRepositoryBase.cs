using Hui.Api.Model.Entity.IEntity;
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
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public class EfCoreRepositoryBase<TDbContext, TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        protected TDbContext Context = null;

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        protected virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EfCoreRepositoryBase(TDbContext dbContext)
        {
            Context = dbContext;
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

        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
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

        public override void AddRange(IEnumerable<TEntity> entity)
        {
            Table.AddRange(entity);
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

        public override void UpdateRange(IEnumerable<TEntity> entitys)
        {
            Table.UpdateRange(entitys);
        }

        public override void RemoveRange(IEnumerable<TEntity> entity)
        {
            Table.RemoveRange(entity);
        }

        public override void Remove(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public override void Remove(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Remove(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Remove(entity);
                return;
            }

            //Could not found the entity, do nothing.
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

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }

        public override int Save()
        {
            return Context.SaveChanges();
        }

        public override async Task<int> SaveAsync()
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
