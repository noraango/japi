using System;
using System.Threading.Tasks;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers
{
    [Route("Storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageRepository _StorageRepository;

        public StorageController(IStorageRepository productRepository)
        {
            _StorageRepository = productRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Read()
        {
            try
            {
                var result = await _StorageRepository.Read();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                int result = await _StorageRepository.Delete(id ?? default(int));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}