using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Datamodel.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null, 
                int? page = null,
                int? pageSize = null,
                SortExpression<T> sortExpressions = null,
                bool trackEntities = true,
                params Expression<Func<T, object>>[] includeProperties);

        Task<T> First(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(int id, params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);
        void Add(params T[] entities);
        void Add(IEnumerable<T> entities);

        void Delete(T entity);
        Task Delete(int id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
        void Detached(T entity);
    }
    
}
