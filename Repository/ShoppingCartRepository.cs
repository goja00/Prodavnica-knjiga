using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class ShoppingCartRepository : Repository<Shopping_Cart>, IShoppingCartRepository
    {
        private DBContext db;
        public ShoppingCartRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }

        public int DecrementCount(Shopping_Cart sc, int count)
        {
            sc.Count -= count;
            return sc.Count;
        }

        public int IncrementCount(Shopping_Cart sc, int count)
        {
            sc.Count += count;
            return sc.Count;
        }
    }
}
