using System.Linq.Expressions;

namespace bookverse.Repository
{
    public interface IRepository<T> where T: class
    {
		IEnumerable<T> GetAll(
			 Expression<Func<T, bool>>? filter = null,
			 Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			 string? includeProperties = null
			 );

		T GetFirstOrDefault(
			Expression<Func<T, bool>>? filter = null,
			string? includeProperties = null
			);
		void Add(T item);
        void Delete(T item);
		void RemoveRange(IEnumerable<T> item);
    }
}
