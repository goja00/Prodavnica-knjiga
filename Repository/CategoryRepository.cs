using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private DBContext db;
        public CategoryRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(Category category)
        {
            db.Update(category);
        }
    }
}
