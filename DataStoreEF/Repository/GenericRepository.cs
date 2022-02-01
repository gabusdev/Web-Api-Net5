using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataEF.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CoreDbContext _context;
        protected readonly DbSet<T> _db;

        public GenericRepository(CoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _db = _context.Set<T>();
        }

        public Task AddAsync(T t)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<T> t)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = AddIncludes(includes);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T> FindOnlyAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            var query = AddIncludes(includes);
            return await query.SingleOrDefaultAsync(match);
        }

        public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = AddIncludes(includes);
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(object id, params Expression<Func<T, object>>[] includes)
        {
            return await _db.FindAsync(id);
        }

        public Task UpdateAsync(T t, object key)
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> AddIncludes(Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            query = query.AsSplitQuery();
            return query;
        }
    }
}