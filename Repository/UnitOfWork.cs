using bookverse.Data;
using bookverse.Models;

namespace bookverse.Repository
{
    public class UnitOfWork:IUnitOfWork
    {   
       public ICategoryRepository categoryRepository { get; private set; }

       public ICoverTypeRepository coverTypeRepository { get; private set; }    

       public IProductRepository productRepository { get; private set; }
        public IShoppingCartRepository shoppingCartRepository { get; private set; }
        public IApplicationUserRepository applicationUserRepository { get; private set; }   

       private DBContext db;

        public UnitOfWork(DBContext _db) {
            db = _db;
            categoryRepository=new CategoryRepository(db);
            coverTypeRepository=new CoverTypeRepository(db);   
            productRepository=new ProductRepository(db);
            shoppingCartRepository=new ShoppingCartRepository(db);
            applicationUserRepository=new ApplicationUserRepository(db);
       
        }
      
        public void save()
        {
           db.SaveChanges();
        }
    }
}
