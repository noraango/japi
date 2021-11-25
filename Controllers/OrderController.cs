using System;
using System.Threading.Tasks;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Hosting;
using api.Configs;

namespace api.Controllers
{
    [Route("Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _OrderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this._OrderRepository = orderRepository;
        }
        [HttpGet]
        [Route("getorders/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var result = await _OrderRepository.GetOrdersByUserId(userId);
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
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] OrderModel model)
        {
            try
            {
                var result = await _OrderRepository.Create(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getorderitems/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(int orderId)
        {
            try
            {
                var result = await _OrderRepository.GetOrderItemsByOrderId(orderId);
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
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromForm] OrderModel model)
        {
            try
            {
                var result = await _OrderRepository.Update(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}