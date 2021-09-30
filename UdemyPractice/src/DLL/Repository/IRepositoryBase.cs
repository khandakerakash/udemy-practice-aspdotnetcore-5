using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DLL.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        IQueryable<T> Queryable(Expression<Func<T, bool>> expression = null);
        Task CreateAsync(T entity);
        Task CreateRangeAsync(List<T> entities);
        Task Update(T entity);
        Task UpdateRange(List<T> entities);
        Task Delete(T entity);
        Task DeleteRange(List<T> entities);
        void Detach(T entity);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<long> CountAsync(Expression<Func<T, bool>> expression = null);
    }

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _context;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
        }
        
        public virtual IQueryable<T> Queryable(Expression<Func<T, bool>> expression = null)
        {
            return expression != null
                ? _context.Set<T>().AsQueryable().Where(expression)
                : _context.Set<T>().AsQueryable()
                    .AsNoTracking();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task CreateRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public virtual async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual async Task UpdateRange(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task DeleteRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
            {
                return await _context.Set<T>().CountAsync(expression);
            }

            return await _context.Set<T>().CountAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}