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

        [HttpGet]
        [Route("detail")]
        public async Task<IActionResult> Detail(int storeId, int? page, int? size)
        {
            try
            {
                int currentPage = page != null ? (int)page : 1;
                int pageSize = size != null ? (int)size : 9;
                var result = await _StorageRepository.Detail(storeId, currentPage, pageSize);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("deleteItem")]
        public async Task<IActionResult> DelteItem(int id)
        {
            try
            {
                var result = await _StorageRepository.DeleteItem(id);
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