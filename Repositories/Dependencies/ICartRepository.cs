using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface ICartRepository
    {
        Task<int> AddItemToCart(int productId, int userId, int quantity);
        Task<IEnumerable<ProductModel>> GetCart(int userId);
         Task<System.Object> GetCartByCartId(int cartId);
         Task<System.Object> GetAllCart(int userId);
        Task<int> DeleteItemFromCart(int productId,int cartId, int userId);
        Task<int> UpdateCartItemQuantity(int productId, int userId, int quantity); Task<System.Object> getCartCount(int userId);
    }
}