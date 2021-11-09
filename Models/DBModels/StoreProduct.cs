namespace api.Models.DBModels{
    public class StoreProduct{
        public int Id {get;set;}
        public int? StoreId {get;set;}
        public int? ProductId {get;set;}
        public int? Quantity {get;set;}
    }
}