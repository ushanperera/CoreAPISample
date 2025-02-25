using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Core.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity Find(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity> FindByAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindByAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity FindIncluding(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindIncludingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAllByPageAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize);
        IEnumerable<TEntity> FindAllIncludingByPage(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize, bool isIgnoreQueryFilter, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindAllIncludingByPageAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindAllIncludingByPageWithGroupBy(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, Expression<Func<TEntity, object>> groupBy, bool orderByAscending, int page, int pageSize, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<IGrouping<object, TEntity>> FindAllIncludingWithGroupBy(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> groupBy, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FindByCompositeKey(object key);
        TEntity Add(TEntity t);
        Task<TEntity> AddAsync(TEntity t);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity t, object key);
        Task<TEntity> UpdateAsync(TEntity t, object key);
        int Count();
        Task<int> CountAsync();
        void Save();
        Task<int> SaveAsync();
        void Dispose();
    }
}
