using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private DBContext db;
        public OrderDetailsRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(OrderDetails orderDetails)
        {
            db.Update(orderDetails);
        }
    }
}
