using System;
namespace api.Models.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? WeekendDelivery { get; set; }
        public DateTime EarliestDeliveryDate { get; set; }
        public DateTime LatestDeliveryDate { get; set; }
        public string Address { get; set; }
        public string WardId { get; set; }
        public string DistrictId { get; set; }
        public string ProvinceId { get; set; }
        public int? OrderStatusId { get; set; }
        public int? ShipperId { get; set; }

        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string CancelReason { get; set; }

         public int? ShopId { get; set; }
    }
}