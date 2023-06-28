using bookverse.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bookverse.Repository
{
    public class Repository<T>:IRepository<T> where T : class 
    {
        private readonly DBContext db;
        private DbSet<T> dbSet;
        public Repository(DBContext _db) {
            db = _db;
            dbSet = db.Set<T>();
        
        }

      public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> q = dbSet;
            q = q.Where(filter);

            return q.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> q = dbSet;

            return q.ToList();
           
        }

        public void Add(T item)
        {
            dbSet.Add(item);
            
        }

        public void Delete(T item)
        {
            dbSet.Remove(item);
        }

    }
}
