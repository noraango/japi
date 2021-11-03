using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface ICartRepository
    {
        Task<int> AddItemToCart(int productId, int cartId, int quantity);
        Task<IEnumerable<Cart>> GetCart(int userId);
        Task<int> DeleteItemFromCart(int ProductId, int CartId);
    }
}