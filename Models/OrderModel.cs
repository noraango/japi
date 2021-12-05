using System;
using api.Models.DBModels;

namespace api.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? WeekendDelivery { get; set; }
        public DateTime EarliestDeliveryDate { get; set; }
        public DateTime LatestDeliveryDate { get; set; }
        public string Address { get; set; }
        public string WardId { get; set; }
        public string Ward { get; set; }
        public string DistrictId { get; set; }
        public string District { get; set; }
        public string ProvinceId { get; set; }
        public string Province { get; set; }
        public int? OrderStatusId { get; set; }
        public int? ShipperId { get; set; }
        public string Status { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class shipperViewOrder
    {
        public User user { get; set; }
        public Order order { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
    }
}