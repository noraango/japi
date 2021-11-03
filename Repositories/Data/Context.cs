using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
        }
        public DbSet<api.Models.DBModels.Category> Category { get; set; }
        public DbSet<api.Models.DBModels.Product> Product { get; set; }
        public DbSet<api.Models.DBModels.Image> Image { get; set; }
        public DbSet<api.Models.DBModels.ProductStatus> ProductStatus { get; set; }
        public DbSet<api.Models.DBModels.ProductPackingMethod> ProductPackingMethod { get; set; }
        public DbSet<api.Models.DBModels.Origin> Origin { get; set; }
        public DbSet<api.Models.DBModels.Storage> Storage { get; set; }
        public DbSet<api.Models.DBModels.Cart> Cart { get; set; }
        public DbSet<api.Models.DBModels.CartItem> CartItem{ get; set; }
        public DbSet<api.Models.DBModels.Order> Order { get; set; }
        public DbSet<api.Models.DBModels.OrderItem> OrderItem{ get; set; }
        public DbSet<api.Models.DBModels.OrderStatus> OrderStatus { get; set; }
        public DbSet<api.Models.DBModels.ProductImage> ProductImage { get; set; }
        public DbSet<api.Models.DBModels.LocationDistrict> LocationDistrict{ get; set; }
        public DbSet<api.Models.DBModels.LocationProvince> LocationProvince { get; set; }
        public DbSet<api.Models.DBModels.LocationWard> LocationWard { get; set; }
        public DbSet<api.Models.DBModels.Shipper> Shipper{ get; set; }
        public DbSet<api.Models.DBModels.ShippingCompany> ShippingCompany { get; set; }
        public DbSet<api.Models.DBModels.Shipper> User{ get; set; }
        public DbSet<api.Models.DBModels.Shipper> UserRole{ get; set; }

    }
}