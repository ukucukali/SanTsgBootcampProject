using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SanTsgBootcampProject.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        //To avoid repeating code and make it more maintainable, these methods are used for CRUD and finding operations
        T Get(int? id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
