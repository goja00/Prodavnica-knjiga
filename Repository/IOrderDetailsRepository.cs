using bookverse.Data;
using bookverse.Models;
using System.Data;

namespace bookverse.Repository
{
    public interface IOrderDetailsRepository:IRepository<OrderDetails>
    {
      void Update(OrderDetails orderDetails);   
    }
}
