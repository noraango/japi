using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        public OrderRepository(Context context)
        {
            this._context = context;
        }

        public async Task<int> Create(OrderModel model)
        {
            try
            {

                if (_context != null)
                {
                    var item = new Order();
                    item.UserId = model.UserId;
                    item.WeekendDelivery = model.WeekendDelivery;
                    item.EarliestDeliveryDate = model.EarliestDeliveryDate;
                    item.LatestDeliveryDate = model.LatestDeliveryDate;
                    item.Address = model.Address;
                    item.WardId = model.WardId;
                    item.ProvinceId = model.ProvinceId;
                    item.DistrictId = model.DistrictId;
                    item.OrderStatusId = model.OrderStatusId;
                    item.ShipperId = model.ShipperId;
                    await _context.Order.AddAsync(item);
                    var order = await _context.Order.FirstOrDefaultAsync(x => x.UserId == model.UserId);
                    var cart = await _context.Cart.FirstOrDefaultAsync(x => x.Id == model.UserId);
                    var cartItems = await _context.CartItem.AsQueryable().Where(x => x.CartId == cart.Id).ToListAsync();
                    foreach (CartItem cartItem in cartItems)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderId = order.Id;
                        orderItem.ProductId = cartItem.ProductId;
                        orderItem.Quantity = cartItem.Quantity;
                        await _context.OrderItem.AddAsync(orderItem);
                    }
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<orderModel>> GetOrderItemsByOrderId(int orderId)
        {
            if (_context != null)
            {
                List<orderModel> result = new List<orderModel>();
                var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
                var orderItems = await _context.OrderItem.AsQueryable().Where(x => x.OrderId == order.Id).ToListAsync();
                foreach (OrderItem item in orderItems)
                {
                    orderModel model = new orderModel();
                    var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    model.Id = product.Id;
                    model.Code = product.Code;
                    model.Name = product.Name;
                    model.Quantity = item.Quantity;
                    model.Price = product.Price;
                    model.DisplayImageName = product.DisplayImageName;
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == product.ProductStatusId);
                    model.Status = status.Name;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }

        public async Task<OrderModel> GetOrderByOrderId(int orderId)
        {
            if (_context != null)
            {
                OrderModel result = new OrderModel();
                var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
                result.Id = order.Id;
                result.UserId = order.UserId;
                result.Address = order.Address;
                result.DistrictId = order.DistrictId;
                result.WardId = order.WardId;
                result.EarliestDeliveryDate = order.EarliestDeliveryDate;
                result.LatestDeliveryDate = order.LatestDeliveryDate;
                result.ProvinceId = order.ProvinceId;
                result.WeekendDelivery = order.WeekendDelivery;
                result.ShipperId = order.ShipperId;
                result.OrderStatusId = order.OrderStatusId;
                var status = await _context.OrderStatus.FirstAsync(x => x.Id == order.OrderStatusId);
                result.Status = status.Name;
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<OrderModel>> GetOrdersByUserId(int userId)
        {
            if (_context != null)
            {
                var result = new List<OrderModel>();
                var source = await _context.Order.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
                foreach (Order item in source)
                {
                    var model = new OrderModel();
                    model.Id = item.Id;
                    model.UserId = item.UserId;
                    model.Address = item.Address;
                    model.DistrictId = item.DistrictId;
                    model.WardId = item.WardId;
                    model.EarliestDeliveryDate = item.EarliestDeliveryDate;
                    model.LatestDeliveryDate = item.LatestDeliveryDate;
                    model.ProvinceId = item.ProvinceId;
                    model.WeekendDelivery = item.WeekendDelivery;
                    model.ShipperId = item.ShipperId;
                    model.OrderStatusId = item.OrderStatusId;
                    var status = await _context.OrderStatus.FirstAsync(x => x.Id == item.OrderStatusId);
                    model.Status = status.Name;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }

        public async Task<int> Update(OrderModel model)
        {
            var dbitem = await _context.Order.FirstOrDefaultAsync(item => item.OrderStatusId == model.Id);
            if (dbitem != null)
            {
                dbitem.OrderStatusId = model.OrderStatusId;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}