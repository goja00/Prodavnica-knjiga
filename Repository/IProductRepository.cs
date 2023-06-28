using bookverse.Models;

namespace bookverse.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product product);
    }
}
