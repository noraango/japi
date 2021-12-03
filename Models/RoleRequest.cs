namespace api.Models.DBModels
{
     public class RoleRequest
    {
        public int Id {get;set;}
        public string Role {get;set;}
        public int UserId {get;set;}
        public string CMTCode {get;set;}
        public string ProvinceID {get;set;}
        public string DistrictID {get;set;}
        public int status {get;set;}
    }

    public class RoleRequestAdmin{
        public RoleRequest request {get;set;}
        public User infor {get;set;}
        public string Province {get;set;}
        public string District {get;set;}
    }
}