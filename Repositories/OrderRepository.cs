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

        public Task<int> Create(OrderModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetOrderItemsByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderModel>> GetOrdersByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(OrderModel model)
        {
            throw new NotImplementedException();
        }
    }
}