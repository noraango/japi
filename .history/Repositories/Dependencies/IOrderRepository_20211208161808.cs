using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface IOrderRepository
    {
        Task<int> Create(OrderModel model);
        Task<IEnumerable<OrderModel>> GetOrdersByUserId(int userId);
        Task<IEnumerable<ProductModel>> GetOrderItemsByOrderId(int orderId);
        Task<int> Update(OrderModel model);

        Task<System.Object> GetOrderInLocation(int userId, int page, int size);
        Task<System.Object> GetMoreOrder(int userId, int page, int size);
        Task<System.Object> ReceiveOrder(int userId, int orderId);
        Task<System.Object> UpdateOrderStatus(Order order);
        Task<System.Object> ShipperHistoty(int userId, int page, int size);
        Task<System.Object> GetProductItem(int orderId);
        Task<System.Object> CustomerCancel(Order order);
        Task<object> ShippingOrder(int userId, int currentPage, int pageSize);

        Task<System.Object> CancelOrder(Order order);

         Task<System.Object> ViewOrder(Order order);

          Task<System.Object> BuyProduct(int productId,int quantity,int userId);
    }
}