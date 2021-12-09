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
        public async Task<IActionResult> Register(string email,string password,string name,string phone)
        {
            try
            {
                var result = await _UserRepository.Register(new Models.DBModels.User(){
                    Email = email,
                    EncodedPassword = password,
                    FirstName = name,
                    Phone = phone
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("checkEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            try
            {
                var result = await _UserRepository.CheckEmail(email);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("forgotPass")]
        public async Task<IActionResult> ForgotPass(string email)
        {
            try
            {
                var result = await _UserRepository.ForgotPass(email);
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