using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Web_Api_Net5.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> GetAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task AddAsync(T t);
        Task AddRangeAsync(List<T> t);
        Task<int> CountAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T t, object key);
        void Delete(T entity);
        
        T Get(int id);
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Update(T t);
    }
}