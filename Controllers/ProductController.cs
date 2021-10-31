using System;
using System.Threading.Tasks;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;

        public ProductController(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }
        [HttpGet]
        [Route("getlist")]
        public async Task<IActionResult> GetProducts(int quantity)
        {
            try
            {
                var result = await _ProductRepository.GetProducts(quantity);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string querySearch, int? CurrentPage, int? PageSize)
        {
            try
            {
                int currentPage = CurrentPage != null ? (int)CurrentPage : 1;
                int pageSize = PageSize != null ? (int)PageSize : 9;
                var gameModelList = await _ProductRepository.Search(querySearch, currentPage, pageSize);
                if (gameModelList == null)
                {
                    return NotFound();
                }
                return Ok(gameModelList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}