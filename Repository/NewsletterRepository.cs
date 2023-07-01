using bookverse.Data;
using bookverse.Models;

namespace bookverse.Repository
{
	public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
	{
		private DBContext db;
		public NewsletterRepository(DBContext _db) : base(_db)
		{
			db = _db;
		}

		public void Update(Newsletter newsletter)
		{
			db.Update(newsletter);
		}
	}
}
