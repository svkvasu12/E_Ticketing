﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace New_Final_ET1.Data.Base
{
   
        public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
        {
            Task<IEnumerable<T>> GetAllAsync();
            Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
            Task<T> GetByIdAsync(int id);
            
            Task AddAsync(T entity);
            Task UpdateAsync(int id, T entity);
            Task DeleteAsync(int id);
        }
    
}
