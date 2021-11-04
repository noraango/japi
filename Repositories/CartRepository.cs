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
        public async Task<IEnumerable<CartItem>> GetCart(int userId)
        {
            if (_context != null)
            {
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId);
                var result = await _context.CartItem.AsQueryable().Where(x => x.CartId == cart.Id).ToListAsync();
                return result;
            }
            return null;
        }
        public async Task<int> AddItemToCart(int productId, int userId, int quantity)
        {
            var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId);
            var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == productId);
            if (quantity > product.Quantity)
            {
                return 0;
            }
            else
            {
                CartItem item = new CartItem();
                item.CartId = cart.Id;
                item.ProductId = product.Id;
                item.Quantity = product.Quantity;
                await _context.CartItem.AddAsync(item);
                return await _context.SaveChangesAsync();
            }
        }
        public async Task<int> DeleteItemFromCart(int productId, int cartId)
        {
            int result = 0;
            var cartItems = _context.CartItem.AsQueryable().Where(x => x.CartId==cartId).AsQueryable();
            var cartItem= await cartItems.FirstOrDefaultAsync(x => x.ProductId==productId);
            if (cartItem != null)
            {
                _context.CartItem.Remove(cartItem);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }
    }
}