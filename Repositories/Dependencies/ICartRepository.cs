using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface ICartRepository
    {
        Task<int> AddItemToCart(int productId, int userId, int quantity);
        Task<IEnumerable<orderModel>> GetCart(int userId);
        Task<int> DeleteItemFromCart(int productId, int userId);
        Task<int> UpdateCartItemQuantity(int productId, int userId, int quantity);
    }
}