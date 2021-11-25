using System;
namespace api.Models.DBModels
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Rating { get; set; }
        public DateTime RateTime { get; set; }
        public string Comment { get; set; }
        public int? UserId { get; set; }
    }
}