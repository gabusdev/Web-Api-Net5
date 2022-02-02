﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataEF.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(object id, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task InsertAsync(T t);
        Task InsertRangeAsync(List<T> t);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FindOnlyAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        void Update(T t);
        void DeleteAsync(object id);
        void DeleteRange(IEnumerable<T> entities);
    }
}