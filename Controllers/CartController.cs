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
    public class CartController: ControllerBase
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
    }
}