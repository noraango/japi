using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace api.Controllers
{
    [Route("DataRaw")]
    [ApiController]
    public class DataRawController : ControllerBase
    {
        public DataRawController()
        {
            
        }
        [HttpGet]
        [Route("checkCMT")]
        public async Task<IActionResult> CheckVacxin(Double CMTCode)
        {
            try
            {
               if((CMTCode%2==0)){
                   return Ok(2); 
               }else
                return Ok((new Random()).Next(0,2));  
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
       
    }
}