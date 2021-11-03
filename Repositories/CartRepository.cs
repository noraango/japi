using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;
        public CartRepository(Context context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Cart>> GetCart(int userId)
        {
            if (_context != null)
            {
                var result= await _context.Cart.ToListAsync();
                return result;
            }
            return null;
        }
        public async Task<int> AddItemToCart(int productId, int cartId, int quantity)
        {
            return 0;
        }
        public async Task<int> DeleteItemFromCart(int productId, int cartId)
        {
            return 0;
        }
    }
}