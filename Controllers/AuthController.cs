using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace api.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;

        public AuthController(IUserRepository userRepository)
        {
            this._UserRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var result = await _UserRepository.Login(username, password);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] User user)
        {
            try
            {
                if (user.Email == null || user.Email == "")
                {
                    return Ok(new
                    {
                        status = false,
                        message = "Email must not be empty"
                    });
                }
                if (user.EncodedPassword == null || user.EncodedPassword == "")
                {
                    return Ok(new
                    {
                        status = false,
                        message = "Password must not be empty"
                    });
                }   
                var result = await _UserRepository.Register(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("forget")]
        public async Task<IActionResult> Forget()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}