namespace api.Models.DBModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int? BelongToCategoryId { get; set; }
    }
}