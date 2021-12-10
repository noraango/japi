namespace api.Models.DBModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Size { get; set; }
        public double? Weight { get; set; }
        public string Manufacturer { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int? OriginId { get; set; }
        public int? PackingMethodId { get; set; }
        public int? ProductStatusId { get; set; }
        public string DisplayImageName { get; set; }
         public int? CategoryId { get; set; }
    }
}