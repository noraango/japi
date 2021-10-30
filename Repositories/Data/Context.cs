using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
        }
        public DbSet<api.Models.DBModels.Category> Category { get; set; }
    }
}