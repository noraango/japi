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
        public async Task<IEnumerable<ProductModel>> GetCart(int userId)
        {
            if (_context != null)
            {
                List<ProductModel> result = new List<ProductModel>();
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId);
                var cartItems = await _context.CartItem.AsQueryable().Where(x => x.CartId == cart.Id).ToListAsync();
                foreach (CartItem item in cartItems)
                {
                    ProductModel model = new ProductModel();
                    var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    model.Id = product.Id;
                    model.Code = product.Code;
                    model.Name = product.Name;
                    model.Quantity = item.Quantity;
                    model.Price = product.Price;
                    model.DisplayImageName = product.DisplayImageName;
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == product.ProductStatusId);
                    model.Status = status.Name;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }
        public async Task<int> AddItemToCart(int productId, int userId, int quantity)
        {
            if (_context != null)
            {
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId);
                var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == productId);
                var cartItem = await _context.CartItem.FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cart.Id);

                if (quantity > product.Quantity)
                {
                    return 0;
                }
                else if (cartItem == null)
                {
                    CartItem item = new CartItem();
                    item.CartId = cart.Id;
                    item.ProductId = product.Id;
                    item.Quantity = quantity;
                    await _context.CartItem.AddAsync(item);
                    return await _context.SaveChangesAsync();
                }
                else if (cartItem.Quantity + quantity > product.Quantity)
                {
                    return 0;
                }
                else
                {
                    cartItem.Quantity += quantity;
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
        public async Task<int> DeleteItemFromCart(int productId, int userId)
        {
            int result = 0;
            if (_context != null)
            {
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId);
                var cartItems = _context.CartItem.AsQueryable().Where(x => x.CartId == cart.Id).AsQueryable();
                var cartItem = await cartItems.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (cartItem != null)
                {
                    _context.CartItem.Remove(cartItem);
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<int> UpdateCartItemQuantity(int productId, int userId, int quantity)
        {
            if (_context != null)
            {
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId);
                var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == productId);
                var cartItem = await _context.CartItem.FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cart.Id);
                if (quantity > product.Quantity)
                {
                    return 0;
                }
                else
                {
                    cartItem.Quantity = quantity;
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
    }
}