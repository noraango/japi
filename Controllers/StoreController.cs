using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers
{
    [Route("Store")]
    [ApiController]
    public class Store : ControllerBase
    {
        private readonly IStoreRepository _StorageRepository;

        public Store(IStoreRepository storageRepository)
        {
            _StorageRepository = storageRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] Models.DBModels.Store storage)
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
        public async Task<IActionResult> Update([FromForm] Models.DBModels.Store storage)
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