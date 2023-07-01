using bookverse.Data;
using bookverse.Models;
using System.Data;
using System.Security.AccessControl;

namespace bookverse.Repository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
      void Update(OrderHeader orderHeader);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus=null);
    }
}
