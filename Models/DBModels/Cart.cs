using System;
namespace api.Models.DBModels
{
    public class Cart
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime OrderTime { get; set; }
        public string Note { get; set; }
        public int? UserId { get; set; }
        public int? OrderStatusId { get; set; }

        public int? shopId { get; set; }
    }
}