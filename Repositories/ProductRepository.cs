using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
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

        public async Task<IEnumerable<ProductStatus>> GetStatuses()
        {
            if (_context != null)
            {
                return await _context.ProductStatus.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<ProductPackingMethod>> GetPackMethods()
        {
            if (_context != null)
            {
                return await _context.ProductPackingMethod.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Origin>> GetOrigins()
        {
            if (_context != null)
            {
                return await _context.Origin.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<ProductModel>> GetProducts(int quantity)
        {
            if (_context != null)
            {
                var result = new List<ProductModel>();
                var source = await _context.Product.AsQueryable().Take(quantity).ToListAsync();
                foreach (Product item in source)
                {
                    var model = new ProductModel();
                    model.Id = item.Id;
                    model.Name = item.Name;
                    model.Quantity = item.Quantity;
                    model.Price = item.Price;
                    model.DisplayImageId = item.DisplayImageId;
                    var image = await _context.Image.FirstAsync(x => x.Id == item.DisplayImageId);
                    model.DisplayImageFilePath = image.FilePath;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<ProductModel>> Search(string querySearch, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var result = new List<ProductModel>();
                var source = await _context.Product.AsQueryable().Where(x => x.Name.ToLower().Contains(querySearch.ToLower())).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();
                foreach (Product item in source)
                {
                    var model = new ProductModel();
                    model.Id = item.Id;
                    model.Code = item.Code;
                    model.Name = item.Name;
                    model.Quantity = item.Quantity;
                    model.Price = item.Price;
                    model.DisplayImageId = item.DisplayImageId;
                    var image = await _context.Image.FirstAsync(x => x.Id == item.DisplayImageId);
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == item.ProductStatusId);
                    model.DisplayImageFilePath = image.FilePath;
                    model.Status = status.Name;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }
    }
}