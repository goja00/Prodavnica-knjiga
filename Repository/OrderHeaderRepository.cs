using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bookverse.Repository
{
    public class OrderHeaderRepository: Repository<OrderHeader>, IOrderHeaderRepository
    {
        private DBContext db;
        public OrderHeaderRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }
		
		public void Update(OrderHeader orderHeader)
        {
            db.Update(orderHeader);
        }


		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var obj=db.orderHeaders.FirstOrDefault(x => x.Id == id);
            if (obj != null) 
            {
                obj.OrderStatus = orderStatus;
                if(paymentStatus != null)
                {
                    obj.PaymentStatus = paymentStatus;  
                }
            }
        }
    }
}
