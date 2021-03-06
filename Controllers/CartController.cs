using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace api.Controllers
{
    [Route("Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _CartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this._CartRepository = cartRepository;
        }
        [HttpGet]
        [Route("get/{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            try
            {
                var result = await _CartRepository.GetCart(userId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getList/{userId}")]
        public async Task<IActionResult> GetListCart(int userId)
        {
            try
            {
                var result = await _CartRepository.GetAllCart(userId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getCart/{cartId}")]
        public async Task<IActionResult> getCart(int cartId)
        {
            try
            {
                var result = await _CartRepository.GetCartByCartId(cartId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("add/{productId}/{userId}/{quantity}")]
        public async Task<IActionResult> AddItemToCart(int productId, int userId, int quantity)
        {
            try
            {
                var result = await _CartRepository.AddItemToCart(productId, userId, quantity);
                return Ok(result);  
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("update/{productId}/{userId}/{quantity}")]
        public async Task<IActionResult> updateFromCart(int productId, int userId, int quantity)
        {
            try
            {
                var result = await _CartRepository.UpdateCartItemQuantity(productId, userId, quantity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("delete/{productId}/{cartId}/{userId}")]
        public async Task<IActionResult> DeleteItemFromCart(int productId,int cartId, int userId)
        {
            try
            {
                var result = await _CartRepository.DeleteItemFromCart(productId,cartId, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("countNumber")]
        public async Task<IActionResult> countNumber(int userId)
        {
            try
            {
                var result = await _CartRepository.getCartCount(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}