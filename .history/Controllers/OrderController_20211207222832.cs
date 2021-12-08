using System;
using System.Threading.Tasks;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;
using api.Models;

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
        [HttpGet]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrderForShipper(int userId, int filterType, int page, int size)
        {
            try
            {
                if (filterType == 1)
                {
                    var result = await _OrderRepository.GetOrderInLocation(userId, page, size);
                    return Ok(result);
                }
                else
                {
                    var result = await _OrderRepository.GetMoreOrder(userId, page, size);
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("ViewOrder")]
        public async Task<IActionResult> ViewOrder(int orderId)
        {
            try
            {
                var result = await _OrderRepository.ViewOrder(new Models.DBModels.Order() { Id = orderId });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetHistory")]
        public async Task<IActionResult> GetHistory(int userId, int page, int size)
        {
            try
            {
                var result = await _OrderRepository.ShipperHistoty(userId, page, size);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetShipping")]
        public async Task<IActionResult> GetShipping(int userId, int page, int size)
        {
            try
            {
                var result = await _OrderRepository.ShippingOrder(userId, page, size);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("ReceiveOrder")]
        public async Task<IActionResult> ReceiveOrder(int userId, int orderId)
        {
            try
            {
                var result = await _OrderRepository.ReceiveOrder(userId, orderId);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateStatusOrder(int orderId, int status)
        {
            try
            {
                var result = await _OrderRepository.UpdateOrderStatus(new Models.DBModels.Order()
                {
                    Id = orderId,
                    OrderStatusId = status
                });
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int cancelType, int orderId, string reason)
        {
            try
            {
                if (cancelType == 1)
                {
                    var result = await _OrderRepository.CancelOrder(new Models.DBModels.Order()
                    {
                        Id = orderId,
                        CancelReason = reason
                    });
                    return Ok(result);
                }
                else
                {
                    var result = await _OrderRepository.CustomerCancel(new Models.DBModels.Order()
                    {
                        Id = orderId,
                        CancelReason = reason
                    });
                    return Ok(result);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}