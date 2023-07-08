using bookverse.Data;
using bookverse.Models;
using System.Data;

namespace bookverse.Repository
{
    public interface ICustomerMessageRepository:IRepository<CustomerMessage>
    {
		void Update(CustomerMessage customerMessage);
	}
}
