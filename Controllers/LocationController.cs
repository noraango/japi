using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
namespace api.Controllers
{
    [Route("Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
         private readonly ILocationRepository _LocationRepository;

        public LocationController(ILocationRepository locationRepository )
        {
            this._LocationRepository=locationRepository;
        }
        [HttpGet]
        [Route("province")]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var result = await _LocationRepository.GetProvince();
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
        [Route("district/{provinceId}")]
        public async Task<IActionResult> GetDistrict(string provinceId)
        {
            try
            {
                var result = await _LocationRepository.GetDistrict(provinceId);
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
        [Route("ward/{districtId}")]
        public async Task<IActionResult> GetWard(string districtId)
        {
            try
            {
                var result = await _LocationRepository.GetWard(districtId);
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