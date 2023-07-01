using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace bookverse.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository categoryRepository { get;  }
        ICoverTypeRepository coverTypeRepository { get; }
        IProductRepository productRepository{ get; }
        IApplicationUserRepository applicationUserRepository { get; }   
        IShoppingCartRepository shoppingCartRepository { get; } 
        INewsletterRepository newsletterRepository { get; }
        IOrderDetailsRepository orderDetailsRepository { get; }
        IOrderHeaderRepository orderHeaderRepository { get; }   
        void save();
    }
}
