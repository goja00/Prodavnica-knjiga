using bookverse.Models;

namespace bookverse.Repository
{
	public interface INewsletterRepository:IRepository<Newsletter>
	{
		void Update(Newsletter newsletter);
	}
}
