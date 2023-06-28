using bookverse.Data;
using bookverse.Models;
using System.Data;

namespace bookverse.Repository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);

    }
}
