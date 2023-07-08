using bookverse.Data;
using bookverse.Models;

namespace bookverse.Repository
{
	public class CustomerMessageRepository : Repository<CustomerMessage>, ICustomerMessageRepository
	{
		private DBContext db;
		public CustomerMessageRepository(DBContext _db) : base(_db)
		{
			db = _db;
		}

		public void Update(CustomerMessage customerMessage)
		{
			db.Update(customerMessage);
		}
	}
}
