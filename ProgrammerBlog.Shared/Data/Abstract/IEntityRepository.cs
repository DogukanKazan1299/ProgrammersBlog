﻿using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Shared.Data.Abstract
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter,params Expression<Func<T,object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T,bool>> filter);
        Task<int> CountAsync(Expression<Func<T,bool>> filter);

    }
}
