using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SanTsgBootcampProject.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext _context;
        //call context
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        //used when needed to add one new entity 
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        //used when needed to add more than one new entity
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter);
        }

        //returns entity by id
        public T Get(int? id)
        {
            return _context.Set<T>().Find(id);
        }

        //returs all context of specific entity
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        //used for hard remove of one entity
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        //used for hard remove of more than one entity
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
