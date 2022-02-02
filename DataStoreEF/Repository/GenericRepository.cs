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

        public async Task InsertAsync(T t)
        {
            await _db.AddAsync(t);
        }

        public async Task InsertRangeAsync(List<T> t)
        {
            await _db.AddRangeAsync(t);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate is null
                ? await _db.CountAsync()
                : await _db.CountAsync(predicate);
        }

        public async void DeleteAsync(object id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
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
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(object id, params Expression<Func<T, object>>[] includes)
        {
            return await _db.FindAsync(id);
        }

        public void Update(T t)
        {
            _db.Attach(t);
            _context.Entry(t).State= EntityState.Modified;
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