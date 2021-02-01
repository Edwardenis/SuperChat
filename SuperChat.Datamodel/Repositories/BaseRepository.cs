using Microsoft.EntityFrameworkCore;
using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Datamodel.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        public readonly IUnitOfWork<SuperChatDbContext> _uow;
        public readonly SuperChatDbContext _context;
        public readonly DbSet<T> _dbSet;
        public BaseRepository(IUnitOfWork<SuperChatDbContext> uow)
        {
            _uow = uow;
            _context = _uow.Context;
            _dbSet = _context.Set<T>();
        }

        public Task<T> First(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> list = _dbSet.AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                list = list.Include(includeProperty);
            }
            return list.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null,
                int? page = null,
                int? pageSize = null,
                SortExpression<T> sortExpressions = null,
                bool trackEntities = true,
                params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (!trackEntities)
                query = query.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (sortExpressions != null)
            {
                IOrderedQueryable<T> orderedQuery = null;
                
                if (sortExpressions.SortDirection == ListSortDirection.Ascending)
                {
                    orderedQuery = query.OrderBy(sortExpressions.SortBy);
                }
                else
                {
                    orderedQuery = query.OrderByDescending(sortExpressions.SortBy);
                }
                    

                if (page != null)
                {
                    query = orderedQuery.Skip(((int)page - 1) * (int)pageSize);
                }
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }


            return await query.ToListAsync();
        }
        public virtual Task<T> GetById(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> list = _dbSet.AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                list = list.Include(includeProperty);
            }

            return list.FirstOrDefaultAsync(x => x.Id == id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Add(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }
        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _dbSet.Remove(entity);
        }
        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Update(params T[] entities)
        {
            foreach (T entity in entities)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public virtual void Detached(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
