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
        public INewsletterRepository newsletterRepository { get; private set; }

        public IOrderDetailsRepository orderDetailsRepository {get; private set;}

        public IOrderHeaderRepository orderHeaderRepository { get; private set; }
        public ICustomerMessageRepository customerMessageRepository { get; private set; }

        private DBContext db;

        public UnitOfWork(DBContext _db) {
            db = _db;
            categoryRepository=new CategoryRepository(db);
            coverTypeRepository=new CoverTypeRepository(db);   
            productRepository=new ProductRepository(db);
            shoppingCartRepository=new ShoppingCartRepository(db);
            applicationUserRepository=new ApplicationUserRepository(db);
            newsletterRepository=new NewsletterRepository(db);
            orderDetailsRepository = new OrderDetailsRepository(db);
            orderHeaderRepository = new OrderHeaderRepository(db);
            customerMessageRepository = new CustomerMessageRepository(db);
        }
      
        public void save()
        {
           db.SaveChanges();
        }
    }
}
