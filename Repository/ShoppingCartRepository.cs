using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private DBContext db;
        public ShoppingCartRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }

  
    }
}
