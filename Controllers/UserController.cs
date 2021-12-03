using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace api.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository userRepository)
        {
            this._UserRepository = userRepository;
        }
        [HttpPost]
        [Route("ShipperResgister")]
        public async Task<IActionResult> ShipperRegister(int userId, string CMTCode, string provideId, string districtId)
        {
            try
            {
                var result = await _UserRepository.RoleRegister(new RoleRequest()
                {
                    UserId = userId,
                    CMTCode = CMTCode.Trim(),
                    Role = "U5",
                    ProvinceID = provideId.Trim(),
                    DistrictID = districtId.Trim(),
                    status = 0
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("ShopResgister")]
        public async Task<IActionResult> ShopRegister(int userId, string CMTCode, string provideId, string districtId)
        {
            try
            {
                var result = await _UserRepository.RoleRegister(new RoleRequest()
                {
                    UserId = userId,
                    CMTCode = CMTCode.Trim(),
                    Role = "U4".Trim(),
                    ProvinceID = provideId.Trim(),
                    DistrictID = districtId.Trim(),
                    status = 0
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("RoleRequest")]
        public async Task<IActionResult> GetRequest(int page, int size)
        {
            try
            {
                var result = await _UserRepository.GetRequest(page, size);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("UserRequest")]
        public async Task<IActionResult> GetUser(int page, int size, int roleId, int status)
        {
            try
            {
                var result = await _UserRepository.FilterUser(page, size, roleId, status);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("UpdateRequest")]
        public async Task<IActionResult> UpdateRequest(int requestId, int status)
        {
            try
            {
                var result = await _UserRepository.UpdateRoleRequest(new RoleRequest() { Id = requestId, status = status });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("UpdateUserStatus")]
        public async Task<IActionResult> UpdateStatusUser(int userId, int status)
        {
            try
            {
                var result = await _UserRepository.UpdateUser(new User() { UserId = userId, Status = status });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("ViewRole")]
        public async Task<IActionResult> ViewRole(int userId)
        {
            try
            {
                var result = await _UserRepository.viewRole(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}