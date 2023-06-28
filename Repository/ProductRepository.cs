using bookverse.Data;
using bookverse.Models;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private DBContext db;
        public ProductRepository(DBContext _db) : base(_db)
        {
            db= _db;
        }

        public void Update(Product product)
        {
			var temp = db.products.FirstOrDefault(x => x.Id == product.Id);
			if(temp!= null) 
			{ 
			temp.Name = product.Name;
			temp.Description = product.Description;
			temp.Price = product.Price;
			temp.CategoryID = product.CategoryID;
			temp.CoverTypeID = product.CoverTypeID;
			temp.Author = product.Author;

			if (product.ImageURL != null)
			{
				temp.ImageURL = product.ImageURL;
			}
			db.products.Update(temp);
		}

        }
    }
}
