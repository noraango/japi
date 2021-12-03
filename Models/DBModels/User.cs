namespace api.Models.DBModels
{
    public class User
    {
        public int UserId { get; set; }
        public int? UserRoleId { get; set; }
        public string EncodedPassword { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AvatarFilename { get; set; }
        public string WardId { get; set; }
        public string DistrictId { get; set; }
        public string ProvinceId { get; set; }
         public int Status { get; set; }
    }
}