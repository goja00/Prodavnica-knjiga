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

        void save();
    }
}
