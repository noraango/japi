namespace api.Models.DBModels
{
    public class Store
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double? Square { get; set; }
        public int? Floor { get; set; }
        public string Address { get; set; }
        public string VillageId { get; set; }
        public string WardId { get; set; }
        public string DistrictId { get; set; }
        public string ProvinceId { get; set; }
    }
}