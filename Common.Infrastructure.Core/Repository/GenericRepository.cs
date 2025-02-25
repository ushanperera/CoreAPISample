using Common.Infrastructure.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Core.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll().AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll().AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return await queryable.ToListAsync();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return context.Set<TEntity>().SingleOrDefault(match);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return context.Set<TEntity>().Where(match).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllByPageAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize)
        {
            if (orderByAscending)
            {
                return await context.Set<TEntity>().OrderBy(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await context.Set<TEntity>().OrderByDescending(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public virtual IEnumerable<TEntity> FindAllIncludingByPage(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize, bool isIgnoreQueryFilter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query;

            if (isIgnoreQueryFilter)
            {
                if (orderByAscending)
                {
                    query = context.Set<TEntity>().IgnoreQueryFilters().OrderBy(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
                }
                else
                {
                    query = context.Set<TEntity>().IgnoreQueryFilters().OrderByDescending(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
                }
            }

            else
            {
                if (orderByAscending)
                {
                    query = context.Set<TEntity>().OrderBy(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
                }
                else
                {
                    query = context.Set<TEntity>().OrderByDescending(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
                }
            }

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllIncludingByPageAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query;
            if (orderByAscending)
            {
                query = context.Set<TEntity>().OrderBy(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = context.Set<TEntity>().OrderByDescending(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize);
            }

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }


        public virtual IEnumerable<TEntity> FindAllIncludingByPageWithGroupBy(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, Expression<Func<TEntity, object>> groupBy, bool orderByAscending, int page, int pageSize, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query;
            if (orderByAscending)
            {
                query = context.Set<TEntity>().OrderBy(orderBy).Where(match);
            }
            else
            {
                query = context.Set<TEntity>().OrderByDescending(orderBy).Where(match);
            }

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.GroupBy(groupBy.Compile()).Select(x => x.FirstOrDefault()).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public virtual IEnumerable<IGrouping<object, TEntity>> FindAllIncludingWithGroupBy(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> groupBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(match);
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.GroupBy(groupBy.Compile());
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().Where(match).ToListAsync();
        }

        public virtual IEnumerable<TEntity> FindByAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(predicate).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual async Task<IEnumerable<TEntity>> FindByAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(predicate).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public virtual TEntity FindIncluding(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(match).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();
        }

        public virtual async Task<TEntity> FindIncludingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(match).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync();
        }

        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> query = context.Set<TEntity>().Where(predicate);
            return query;
        }

        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual TEntity FindByCompositeKey(object key)
        {
            TEntity query = context.Set<TEntity>().Find(key);
            return query;
        }

        public virtual TEntity Add(TEntity t)
        {

            context.Set<TEntity>().Add(t);
            return t;
        }

        public virtual async Task<TEntity> AddAsync(TEntity t)
        {
            await context.Set<TEntity>().AddAsync(t);
            return t;

        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual TEntity Update(TEntity t, object key)
        {
            if (t == null)
            {
                return null;
            }

            TEntity exist = context.Set<TEntity>().Find(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity t, object key)
        {
            if (t == null)
            {
                return null;
            }

            TEntity exist = await context.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        public virtual int Count()
        {
            return context.Set<TEntity>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await context.Set<TEntity>().CountAsync();
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }
    }
}
