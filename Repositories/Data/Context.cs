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
    }
}