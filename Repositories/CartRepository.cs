using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;
        public CartRepository(Context context)
        {
            this._context = context;
        }

        public async Task<System.Object> GetCartByCartId(int cartId)
        {
            if (_context != null)
            {
                List<ProductModel> result = new List<ProductModel>();
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.Id == cartId);
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

        public async Task<System.Object> GetAllCart(int userId)
        {
            if (_context != null)
            {
                List<Cart> result = await _context.Cart.Where(x => x.UserId == userId).ToListAsync();
                return new
                {
                    data = result
                };
            }
            return null;
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
                var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == productId);
                if (product == null)
                {
                    return 0;
                }
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId && x.shopId == product.shopId);
                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    cart.OrderTime = System.DateTime.Now;
                    cart.OrderStatusId = 1;

                    cart.shopId = product.shopId;
                    await _context.Cart.AddAsync(cart);
                    int index = _context.SaveChanges();
                }
                var cartItem = await _context.CartItem.FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cart.Id);

                if (cartItem == null)
                {
                    CartItem item = new CartItem();
                    item.CartId = cart.Id;
                    item.ProductId = productId;
                    item.Quantity = quantity;
                    await _context.CartItem.AddAsync(item);
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    cartItem.Quantity += quantity;
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
        public async Task<int> DeleteItemFromCart(int productId, int cartId, int userId)
        {
            int result = 0;
            if (_context != null)
            {
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.Id == cartId);
                var cartItems = await _context.CartItem.Where(x => x.CartId == cart.Id).ToListAsync();

                if (cartItems != null)
                {
                    if (cartItems.Count == 1)
                    {
                        _context.CartItem.Remove(cartItems[0]);
                        _context.Cart.Remove(cart);
                    }
                    else
                    {
                        _context.CartItem.Remove(cartItems.Where(x => x.ProductId == productId).First());
                    }
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<int> UpdateCartItemQuantity(int productId, int userId, int quantity)
        {
            var result = 0;
            if (_context != null)
            {
                var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == productId);
                var cart = await _context.Cart.FirstOrDefaultAsync(x => x.UserId == userId && x.shopId == product.shopId);
                var cartItem = await _context.CartItem.FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cart.Id);
                cartItem.Quantity = quantity;
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

         public async Task<System.Object> getCartCount(int userId)
        {

            if (_context != null)
            {
                var result = 0;
                var order = await _context.Cart.Where(x => x.UserId == userId).ToListAsync();
                foreach(var item in order){
                   var cart = await _context.CartItem.Where(x=>x.CartId == item.Id).ToListAsync();
                   foreach(var c in cart){
                       result+=(int)c.Quantity;
                   }
                }
                return new
                {
                    data = result
                };
            }
            return null;
        }
    }
}