using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class CoverTypeRepository : Repository<CoverType>,ICoverTypeRepository
    {
        private DBContext db;
        public CoverTypeRepository(DBContext _db) : base(_db)
        {
            db = _db;
        }

         public void Update(CoverType coverType)
        {
            db.Update(coverType);
        }
    }
}
