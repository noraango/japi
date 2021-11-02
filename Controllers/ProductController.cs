using System;
using System.Threading.Tasks;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Hosting;
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
            // string code, string name, decimal? price, double? size, double? weight, int? quantity, string manufacturer
            //                                             , string shortDes, string des, string brand, int? originId, int? packingMethodId
            //                                             , int? statusId, int? storageId, [FromForm] ImageFileModel imageFile
            try
            {
                // var item = new ProductModel();
                // item.Code = code;
                // item.Name = name;
                // item.Price = price;
                // item.Size = size;
                // item.Weight = weight;
                // item.Quantity = quantity;
                // item.Manufacturer = manufacturer;
                // item.ShortDescription = shortDes;
                // item.Description = des;
                // item.Brand = brand;
                // item.OriginId = originId;
                // item.PackingMethodId = packingMethodId;
                // item.ProductStatusId = statusId;
                // item.StorageId = storageId;
                // item.DisplayImageName = await FileHandler.SaveImage(imageFile.imageFile, _HostEnvironment.ContentRootPath);
                productModel.DisplayImageName = await FileHandler.SaveImage(productModel.DisplayImage, _HostEnvironment.ContentRootPath);
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