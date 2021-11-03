using System;
namespace api.Models.DBModels
{
    public class Cart
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime OrderTime { get; set; }
        public string Note { get; set; }
        public int? UserID { get; set; }
        public int? OrderStatusID { get; set; }
    }
}