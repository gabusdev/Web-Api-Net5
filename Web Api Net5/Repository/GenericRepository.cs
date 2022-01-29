using DataStoreEF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Web_Api_Net5.Repository
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

        public IQueryable<T> GetAll()
        {
            return _db;
        }

        public T Get(int id)
        {
            return _db.Find(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _db.SingleOrDefault(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _db.Where(match).ToList();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _db.Where(predicate);
            return query;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task AddAsync(T t)
        {
            await _db.AddAsync(t);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _db.SingleOrDefaultAsync(match);
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _db.Where(match).ToListAsync();
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task UpdateAsync(T t, object key)
        {
            T exist = null;

            if (key.GetType() == typeof(int[]))
            {
                exist = await _db.FindAsync(((int[])key)[0], ((int[])key)[1]);
            }
            else
            {
                exist = await _db.FindAsync(key);
            }
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
            }
        }

        public void Update(T t)
        {
            _db.Update(t);
        }

        public async Task<int> CountAsync()
        {
            return await _db.CountAsync();
        }

        public async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        public async Task AddRangeAsync(List<T> t)
        {
            await _db.AddRangeAsync(t);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            T query = _db.FirstOrDefault(predicate);
            return query;
        }
    }
}