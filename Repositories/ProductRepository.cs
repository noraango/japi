using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts(int quantity)
        {
            if (_context != null)
            {
                return await _context.Product.AsQueryable().Take(quantity).ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Product>> Search(string querySearch, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var result = new List<Product>();
                var source = _context.Product.AsQueryable();
                if (!string.IsNullOrEmpty(querySearch))
                {
                    source = source.Where(x => x.Name.ToLower().Contains(querySearch.ToLower())).AsQueryable();
                }
                return await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return null;
        }
    }
}