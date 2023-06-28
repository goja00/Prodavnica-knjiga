using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private DBContext db;
        public ApplicationUserRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }

 
    }
}
