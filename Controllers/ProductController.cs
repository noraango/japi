using System;
using System.Threading.Tasks;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using api.Configs;
namespace api.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IWebHostEnvironment _HostEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment hostEnvironment)
        {
            _ProductRepository = productRepository;
            _HostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var result = await _ProductRepository.GetStatuses();
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
        [Route("packing")]
        public async Task<IActionResult> GetPacking()
        {
            try
            {
                var result = await _ProductRepository.GetPackMethods();
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
        [Route("origin")]
        public async Task<IActionResult> GetOrigin()
        {
            try
            {
                var result = await _ProductRepository.GetOrigins();
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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductModel productModel)
        {
            try
            {
                var listImages = Request.Form.Files;
                List<string> imageNames = new List<string>();
                for (int i = 1; i < listImages.Count; i++)
                {
                    imageNames.Add(await FileHandler.SaveImage(listImages[i], _HostEnvironment.ContentRootPath));
                }
                productModel.DisplayImageName = await FileHandler.SaveImage(productModel.DisplayImage, _HostEnvironment.ContentRootPath);
                productModel.imageNames = imageNames;
                var result = await _ProductRepository.Create(productModel);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
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
                if (string.IsNullOrWhiteSpace(querySearch))
                {
                    querySearch = "";
                }
                int currentPage = CurrentPage != null ? (int)CurrentPage : 1;
                int pageSize = PageSize != null ? (int)PageSize : 9;
                var result = await _ProductRepository.Search(querySearch, currentPage, pageSize);
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
        [Route("get/{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                var result = await _ProductRepository.Detail(id ?? default(int));
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
        [Route("getImages/{id}")]
        public async Task<IActionResult> DetailImages(int? id)
        {
            try
            {
                var result = await _ProductRepository.DetailImages(id ?? default(int));
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
        [Route("getComment/{id}/{page}/{size}")]
        public async Task<IActionResult> getComment(int? id, int? page, int? size)
        {
            try
            {
                var result = await _ProductRepository.GetComment(id ?? default(int), page ?? default(int), size ?? default(int));
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
        [Route("comments")]
        public async Task<IActionResult> Comments(int? id, int? CurrentPage, int? PageSize)
        {
            try
            {
                int currentPage = CurrentPage != null ? (int)CurrentPage : 1;
                int pageSize = PageSize != null ? (int)PageSize : 9;
                var result = await _ProductRepository.getComments(id ?? default(int), currentPage, pageSize);
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
        [Route("category/{categoryId}/{page}/{size}")]
        public async Task<IActionResult> Category(int categoryID, int page, int size)
        {
            try
            {
                var result = _ProductRepository.GetProductsByCategory(categoryID, page, size);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}