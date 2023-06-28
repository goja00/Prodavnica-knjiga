using System.Linq.Expressions;

namespace bookverse.Repository
{
    public interface IRepository<T> where T: class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Delete(T item);
        
    }
}
