using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MailKit.Net.Smtp;
using MimeKit;
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
                    if (model.UserId != null)
                    {
                        Guid g = Guid.NewGuid();
                        var item = new Order();
                        item.Guid = g.ToString();
                        item.UserId = model.UserId;
                        item.WeekendDelivery = model.WeekendDelivery;
                        item.EarliestDeliveryDate = model.EarliestDeliveryDate;
                        item.LatestDeliveryDate = model.LatestDeliveryDate;
                        item.Address = model.Address;
                        item.WardId = model.WardId;
                        item.OrderStatusId = 1;
                        item.WeekendDelivery = true;
                        item.ProvinceId = model.ProvinceId;
                        item.DistrictId = model.DistrictId;
                        item.ShipperId = model.ShipperId;
                        await _context.Order.AddAsync(item);
                        await _context.SaveChangesAsync();
                        var order = await _context.Order.FirstOrDefaultAsync(x => x.Guid == g.ToString());
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
                        await _context.Database.ExecuteSqlRawAsync("delete from CartItem where CartId = " + cart.Id);
                        await _context.SaveChangesAsync();
                        return 1;
                    }
                    else
                    {
                        var item = new Order();
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<object> CustomerCancel(Order order)
        {
            if (_context != null)
            {
                var data = await _context.Order.FirstOrDefaultAsync(x => x.Id == order.Id);
                data.CancelReason = order.CancelReason;
                data.OrderStatusId = 5;
                return await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<object> GetMoreOrder(int userId, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var dataUser = await _context.RoleRequest.FirstOrDefaultAsync(x => x.UserId == userId && x.status == 1);
                if (dataUser != null)
                {
                    var totalRow = await _context.Order.Where(x => x.ProvinceId.Trim() ==
                   dataUser.ProvinceID.Trim() && x.OrderStatusId == 6).CountAsync();
                    var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                    var order = await _context.Order.Where(x => x.ProvinceId.Trim() ==
                    dataUser.ProvinceID.Trim() && x.OrderStatusId == 6).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize).OrderBy(x => x.Id).ToListAsync();

                    List<shipperViewOrder> result = new List<shipperViewOrder>();
                    foreach (var item in order)
                    {
                        var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                        var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == item.DistrictId.Trim()).FirstOrDefaultAsync();
                        var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == item.ProvinceId.Trim()).FirstOrDefaultAsync();
                        result.Add(new shipperViewOrder()
                        {
                            user = user,
                            District = district.Name,
                            Province = city.Name,
                            order = item
                        });
                    }
                    return new
                    {
                        totalPage = totalPage,
                        totalRow = totalRow,
                        data = result
                    };
                }

            }
            return null;
        }

        public async Task<object> GetOrderInLocation(int userId, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var dataUser = await _context.RoleRequest.FirstOrDefaultAsync(x => x.UserId == userId);
                if (dataUser != null)
                {
                    var totalRow = await _context.Order.Where(x => x.ProvinceId.Trim() == dataUser.ProvinceID.Trim()
                      && x.DistrictId.Trim() == dataUser.DistrictID.Trim() && x.OrderStatusId == 6).CountAsync();
                    var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                    var order = await _context.Order.Where(x => x.ProvinceId.Trim() == dataUser.ProvinceID.Trim()
                    && x.DistrictId.Trim() == dataUser.DistrictID.Trim() && x.OrderStatusId == 6).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();

                    List<shipperViewOrder> result = new List<shipperViewOrder>();
                    foreach (var item in order)
                    {
                        var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                        var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == item.DistrictId.Trim()).FirstOrDefaultAsync();
                        var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == item.ProvinceId.Trim()).FirstOrDefaultAsync();
                        result.Add(new shipperViewOrder()
                        {
                            user = user,
                            District = district.Name,
                            Province = city.Name,
                            order = item
                        });
                    }
                    return new
                    {
                        totalPage = totalPage,
                        totalRow = totalRow,
                        data = result
                    };
                }

            }
            return null;
        }

        public async Task<IEnumerable<ProductModel>> GetOrderItemsByOrderId(int orderId)
        {
            if (_context != null)
            {
                List<ProductModel> result = new List<ProductModel>();
                var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
                var orderItems = await _context.OrderItem.AsQueryable().Where(x => x.OrderId == order.Id).ToListAsync();
                foreach (OrderItem item in orderItems)
                {
                    ProductModel model = new ProductModel();
                    var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    model.Id = product.Id;
                    model.Code = product.Code;
                    model.Name = product.Name;
                    model.Price = product.Price;
                    model.DisplayImageName = product.DisplayImageName;
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == product.ProductStatusId);
                    model.Status = status.Name;
                    var packing = await _context.ProductPackingMethod.FirstOrDefaultAsync(x => x.Id == product.PackingMethodId);
                    model.PackingMethod = packing.Name;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUserId(int userId, int statusId)
        {
            if (_context != null)
            {
                var result = new List<OrderModel>();
                var source = await _context.Order.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
                if (statusId > 0)
                {
                    source = await _context.Order.AsQueryable().Where(x => x.UserId == userId && x.OrderStatusId == statusId).ToListAsync();
                }
                foreach (Order item in source)
                {
                    var model = new OrderModel();
                    model.Id = item.Id;
                    model.UserId = item.UserId;
                    model.Address = item.Address;
                    var district = await _context.LocationDistrict.FirstOrDefaultAsync(x => x.DistrictId == item.DistrictId);
                    model.DistrictId = item.DistrictId;
                    model.District = district.Name;
                    var province = await _context.LocationProvince.FirstOrDefaultAsync(x => x.ProvinceId == item.ProvinceId);
                    model.ProvinceId = item.ProvinceId;
                    model.Province = province.Name;
                    model.EarliestDeliveryDate = item.EarliestDeliveryDate;
                    model.LatestDeliveryDate = item.LatestDeliveryDate;
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

        public async Task<object> GetProductItem(int orderId)
        {
            if (_context != null)
            {
                var order = await _context.OrderItem.Where(x => x.OrderId == orderId).ToListAsync();
                List<Product> list = new List<Product>();
                foreach (var item in order)
                {
                    var pro = await _context.Product.Where(x => x.Id == item.ProductId).FirstOrDefaultAsync();
                    list.Add(pro);
                }
                return new
                {
                    order = order,
                    listProduct = list
                };
            }
            return null;
        }

        public async Task<object> ReceiveOrder(int userId, int orderId)
        {
            if (_context != null)
            {
                var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
                order.ShipperId = userId;
                order.OrderStatusId = 3;
                return await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<object> ShipperHistoty(int userId, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var totalRow = await _context.Order.Where(x => x.ShipperId == userId).CountAsync();
                var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                var order = await _context.Order.Where(x => x.ShipperId == userId).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();

                List<shipperViewOrder> result = new List<shipperViewOrder>();
                foreach (var item in order)
                {
                    var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                    var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == item.DistrictId.Trim()).FirstOrDefaultAsync();
                    var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == item.ProvinceId.Trim()).FirstOrDefaultAsync();
                    result.Add(new shipperViewOrder()
                    {
                        user = user,
                        District = district.Name,
                        Province = city.Name,
                        order = item
                    });
                }
                return new
                {
                    totalPage = totalPage,
                    totalRow = totalRow,
                    data = result
                };


            }
            return null;
        }

        public async Task<object> ShippingOrder(int userId, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var totalRow = await _context.Order.Where(x => x.ShipperId == userId && x.OrderStatusId == 3).CountAsync();
                var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                var order = await _context.Order.Where(x => x.ShipperId == userId && x.OrderStatusId == 3).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();

                List<shipperViewOrder> result = new List<shipperViewOrder>();
                foreach (var item in order)
                {
                    var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                    var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == item.DistrictId.Trim()).FirstOrDefaultAsync();
                    var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == item.ProvinceId.Trim()).FirstOrDefaultAsync();
                    result.Add(new shipperViewOrder()
                    {
                        user = user,
                        District = district.Name,
                        Province = city.Name,
                        order = item
                    });
                }
                return new
                {
                    totalPage = totalPage,
                    totalRow = totalRow,
                    data = result
                };


            }
            return null;
        }

        public async Task<object> CancelOrder(Order order)
        {
            if (_context != null)
            {
                var data = await _context.Order.FirstOrDefaultAsync(x => x.Id == order.Id);
                data.CancelReason = order.CancelReason;
                data.OrderStatusId = 8;
                var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == data.UserId);
                await Send(user.Email, "Đơn Hàng Bị Hủy", "Xin lỗi đơn hàng mã số Jap" + order.Id + " của bạn đã bị hủy bỏ");
                return await _context.SaveChangesAsync();
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

        public async Task<object> UpdateOrderStatus(Order order)
        {
            if (_context != null)
            {
                var data = await _context.Order.FirstOrDefaultAsync(x => x.Id == order.Id);
                data.OrderStatusId = order.OrderStatusId;
                return await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<object> ViewOrder(Order order)
        {
            if (_context != null)
            {
                List<ProductModel> result = new List<ProductModel>();
                var orderItems = await _context.OrderItem.AsQueryable().Where(x => x.OrderId == order.Id).ToListAsync();
                foreach (OrderItem item in orderItems)
                {
                    ProductModel model = new ProductModel();
                    var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    model.Id = product.Id;
                    model.Code = product.Code;
                    model.Name = product.Name;
                    model.Price = product.Price;
                    model.DisplayImageName = product.DisplayImageName;
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == product.ProductStatusId);
                    model.Status = status.Name;
                    var packing = await _context.ProductPackingMethod.FirstOrDefaultAsync(x => x.Id == product.PackingMethodId);
                    model.PackingMethod = packing.Name;
                    result.Add(model);
                }
                return new
                {
                    listPro = result
                };
            }
            return null;
        }

        private Task Send(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("japstorefu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };
            email.InReplyTo = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss");
            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                smtp.Authenticate("japstorefu@gmail.com", "JapStore123");
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            return Task.CompletedTask;
        }

        public async Task<System.Object> BuyProduct(int productId, int quantity, int userId)
        {

            return null;
        }

        public async Task<System.Object> getAllStoreOrder(int userId, int currentPage, int pageSize)
        {

            if (_context != null)
            {
                var totalRow = await _context.Order.Where(x => x.ShopId == userId).CountAsync();
                var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;

                var order = await _context.Order.Where(x => x.ShopId == userId).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();
                return new
                {
                    totalPage = totalPage,
                    totalRow = totalRow,
                    order = order
                };
            }
            return null;
        }

        public async Task<System.Object> getOrderDetail(int id)
        {

            if (_context != null)
            {
                var order = await _context.Order.Where(x => x.Id == id).FirstOrDefaultAsync();
                var buyer = await _context.User.Where(x => x.UserId == order.UserId).FirstOrDefaultAsync();
                var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == order.DistrictId.Trim()).FirstOrDefaultAsync();
                var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == order.ProvinceId.Trim()).FirstOrDefaultAsync();


                List<ProductModel> result = new List<ProductModel>();
                var orderItems = await _context.OrderItem.AsQueryable().Where(x => x.OrderId == id).ToListAsync();
                foreach (OrderItem item in orderItems)
                {
                    ProductModel model = new ProductModel();
                    var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    model.Id = product.Id;
                    model.Code = product.Code;
                    model.Name = product.Name;
                    model.Price = product.Price;
                    model.DisplayImageName = product.DisplayImageName;
                    model.Quantity = item.Quantity;
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == product.ProductStatusId);
                    model.Status = status.Name;
                    var packing = await _context.ProductPackingMethod.FirstOrDefaultAsync(x => x.Id == product.PackingMethodId);
                    model.PackingMethod = packing.Name;
                    result.Add(model);
                }
                return new
                {
                    listPro = result
                };
            }
            return null;
        }
    }
}