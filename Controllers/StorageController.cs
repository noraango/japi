using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers
{
    [Route("Storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageRepository _StorageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            _StorageRepository = storageRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] Storage storage)
        {
            try
            {
                var result = await _StorageRepository.Create(storage);
                return Ok(result);
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
        public async Task<IActionResult> Update([FromForm] Storage storage)
        {
            try
            {
                var result = await _StorageRepository.Update(storage);
                return Ok(result);
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