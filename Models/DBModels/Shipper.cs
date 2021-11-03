namespace api.Models.DBModels{
    public class Shipper{
        public int Id {get;set;}
        public int? CompanyId {get;set;}
        public string Name {get;set;}
        public string Phone {get;set;}
        public string Email {get;set;}
    }
}