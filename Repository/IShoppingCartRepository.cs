using bookverse.Data;
using bookverse.Models;
using System.Data;

namespace bookverse.Repository
{
    public interface IShoppingCartRepository:IRepository<Shopping_Cart>
    {
        int IncrementCount(Shopping_Cart sc, int count);

        int DecrementCount (Shopping_Cart sc, int count);
    }
}
