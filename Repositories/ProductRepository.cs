using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
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
                    model.DisplayImageName = item.DisplayImageName;
                    result.Add(model);
                }
                return result;
            }
            return null;
        }

        public async Task<System.Object> Search(string querySearch, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var list = new List<ProductModel>();

                var fullSource = _context.Product.AsQueryable().Where(x =>
                                x.Name.ToLower().Contains(querySearch.ToLower())
                                || x.Code.ToLower().Contains(querySearch.ToLower())
                                || x.Manufacturer.ToLower().Contains(querySearch.ToLower())
                                || x.Brand.ToLower().Contains(querySearch.ToLower())).AsQueryable();
                var source = await fullSource.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();

                foreach (Product item in source)
                {
                    var model = new ProductModel();
                    model.Id = item.Id;
                    model.Code = item.Code;
                    model.Name = item.Name;
                    model.Quantity = item.Quantity;
                    model.Price = item.Price;
                    model.DisplayImageName = item.DisplayImageName;
                    var status = await _context.ProductStatus.FirstAsync(x => x.Id == item.ProductStatusId);
                    model.Status = status.Name;
                    list.Add(model);
                }
                int numberOfPage = (int)Math.Ceiling(fullSource.Count() / Convert.ToDouble(pageSize));
                return new
                {
                    numberOfPage = numberOfPage,
                    list = list,
                };
            }
            return null;
        }

        public async Task<int> Create(ProductModel item)
        {
            try
            {
                if (_context != null)
                {
                    var product = new Product();
                    product.Code = item.Code;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.Size = item.Size;
                    product.Weight = item.Weight;
                    product.Quantity = item.Quantity;
                    product.Manufacturer = item.Manufacturer;
                    product.ShortDescription = item.ShortDescription;
                    product.Description = item.Description;
                    product.Brand = item.Brand;
                    product.OriginId = item.OriginId;
                    product.PackingMethodId = item.PackingMethodId;
                    product.ProductStatusId = item.ProductStatusId;
                    product.StorageId = item.StorageId;
                    product.DisplayImageName = item.DisplayImageName;
                    await _context.Product.AddAsync(product);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<ProductModel> Detail(int productId)
        {
            if (_context != null)
            {
                var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == productId);
                ProductModel result = new ProductModel();
                result.Id = product.Id;
                result.Code = product.Code;
                result.Name = product.Name;
                result.Price = product.Price;
                result.Size = product.Size;
                result.Weight = product.Weight;
                result.Quantity = product.Quantity;
                result.Manufacturer = product.Manufacturer;
                result.ShortDescription = product.ShortDescription;
                result.Description = product.Description;
                result.Brand = product.Brand;
                result.OriginId = product.OriginId;
                var origin = await _context.Origin.FirstOrDefaultAsync(x => x.Id == product.OriginId);
                result.Origin = origin.Name;
                result.PackingMethodId = product.PackingMethodId;
                var packing = await _context.ProductPackingMethod.FirstOrDefaultAsync(x => x.Id == product.PackingMethodId);
                result.PackingMethod = packing.Name;
                result.DisplayImageName = product.DisplayImageName;
                return result;
            }
            return null;
        }
    }
}