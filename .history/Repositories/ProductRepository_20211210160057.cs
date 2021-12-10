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
                    var pp = await _context.Product.FirstOrDefaultAsync(x => x.Code.Equals(item.Code));
                    if (pp != null)
                    {
                        return -1;
                    }
                    var product = new Product();
                    product.Code = item.Code;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.Size = item.Size;
                    product.Weight = item.Weight;
                    product.Manufacturer = item.Manufacturer;
                    product.ShortDescription = item.ShortDescription;
                    product.Description = item.Description;
                    product.Brand = item.Brand;
                    product.OriginId = item.OriginId;
                    product.PackingMethodId = item.PackingMethodId;
                    product.ProductStatusId = item.ProductStatusId;
                    product.DisplayImageName = item.DisplayImageName;
                    await _context.Product.AddAsync(product);
                    await _context.SaveChangesAsync();
                    var p = await _context.Product.FirstOrDefaultAsync(x => x.Code.Equals(item.Code));
                    foreach (var imageName in item.imageNames)
                    {
                        ProductImage productImage = new ProductImage();
                        productImage.Name = imageName;
                        productImage.ProductId = p.Id;
                        await _context.ProductImage.AddAsync(productImage);
                    }
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception e)
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
                result.Manufacturer = product.Manufacturer;
                result.ShortDescription = product.ShortDescription;
                result.Description = product.Description;
                result.Brand = product.Brand;
                result.OriginId = product.OriginId;
                result.ProductStatusId = product.ProductStatusId;
                var status = await _context.ProductStatus.FirstOrDefaultAsync(x => x.Id == product.ProductStatusId);
                result.Status = status.Name;
                var origin = await _context.Origin.FirstOrDefaultAsync(x => x.Id == product.OriginId);
                result.Origin = origin.Name;
                result.PackingMethodId = product.PackingMethodId;
                var packing = await _context.ProductPackingMethod.FirstOrDefaultAsync(x => x.Id == product.PackingMethodId);
                result.PackingMethod = packing.Name;
                result.DisplayImageName = product.DisplayImageName;
                var ratings = await _context.ProductRating.AsQueryable().Where(x => x.ProductId == productId).Select(x => x.Rating).ToListAsync();
                result.rating = ratings.Average();
                List<string> imageNames = new List<string>();
                var productImages = await _context.ProductImage.AsQueryable().Where(x => x.ProductId == product.Id).ToListAsync();
                foreach (var item in productImages)
                {
                    imageNames.Add(item.Name);
                }
                result.imageNames = imageNames;
                return result;
            }
            return null;
        }
        public async Task<List<string>> DetailImages(int productId)
        {
            if (_context != null)
            {
                List<string> imageNames = new List<string>();
                var productImages = await _context.ProductImage.AsQueryable().Where(x => x.ProductId == productId).ToListAsync();
                foreach (var item in productImages)
                {
                    imageNames.Add(item.Name);
                }
                return imageNames;
            }
            return null;
        }

        public async Task<System.Object> GetComment(int productId, int page, int size)
        {
            if (_context != null)
            {
                var totalRow = await _context.ProductRating.AsQueryable().Where(x => x.ProductId == productId).CountAsync();
                var totalPage = (totalRow % size == 0) ? (totalRow / size) : (totalRow / size) + 1;
                var ratings = await _context.ProductRating.AsQueryable().Where(x => x.ProductId == productId)
                .Skip((page - 1) * size).Take(size).OrderByDescending(x => x.RateTime).ToListAsync();
                return new
                {
                    totalPage = totalPage,
                    totalRow = totalRow,
                    data = ratings
                };
            }
            return null;
        }
        public async Task<System.Object> getComments(int productId, int currentPage, int pageSize)
        {
            if (_context != null)
            {
                var comments = await _context.ProductRating.AsQueryable().Where(x => x.ProductId == productId).Join(_context.User, x => x.UserId, y => y.UserId, (x, y) => new
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Rating = x.Rating,
                    RateTime = x.RateTime,
                    Comment = x.Comment,
                    FirstName = y.FirstName,
                    MiddleName = y.MiddleName,
                    LastName = y.LastName,
                }).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.RateTime).ToListAsync();
                return new
                {
                    comments = comments
                };
            }
            return null;
        }

        public async Task<System.Object> GetProductsByCategory(int categoryId, int currentPage, int pageSize)
        {
            // if (_context != null)
            // {
            //     var source = await _context.Product.FirstOrDefaultAsync(x => x. == categoryId);
            //     var products = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();
            //     return result;
            // }
            return null;
        }

        public async Task<System.Object> getCategory(int categoryId, int page, int size)
        {
            if (_context != null)
            {
                var category = await _context.Category.Where(x => x.Id == categoryId).FirstOrDefaultAsync();
                if (category != null)
                {
                    if (category.Level == 1)
                    {
                        var categoryList = await _context.Category.Where(x => x.BelongToCategoryId == categoryId).Select(x => x.Id).ToListAsync();
                        categoryList.Add(category.Id);
                        if (categoryList.Count() > 0)
                        {
                            var result = await _context.Product.Where(x => categoryList.Contains(x.CategoryId)).Skip((page - 1) * size).Take(size).OrderBy(x => x.Id).ToListAsync();
                            var totalRow = await _context.Product.Where(x => categoryList.Contains(x.CategoryId)).CountAsync();
                            var totalPage = (totalRow % size == 0) ? (totalRow / size) : (totalRow / size) + 1;
                            return new
                            {
                                totalPage = totalPage,
                                totalRow = totalRow,
                                data = result
                            };
                        }
                        else
                        {
                            var result = await _context.Product.Where(x => x.CategoryId == categoryId).Skip((page - 1) * size).Take(size).OrderBy(x => x.Id).ToListAsync();
                            var totalRow = await _context.Product.Where(x => x.CategoryId == categoryId).CountAsync();
                            var totalPage = (totalRow % size == 0) ? (totalRow / size) : (totalRow / size) + 1;
                            return new
                            {
                                totalPage = totalPage,
                                totalRow = totalRow,
                                data = result
                            };
                        }
                    }else{
                        var result = await _context.Product.Where(x => x.CategoryId == categoryId).Skip((page - 1) * size).Take(size).OrderBy(x => x.Id).ToListAsync();
                            var totalRow = await _context.Product.Where(x => x.CategoryId == categoryId).CountAsync();
                            var totalPage = (totalRow % size == 0) ? (totalRow / size) : (totalRow / size) + 1;
                            return new
                            {
                                totalPage = totalPage,
                                totalRow = totalRow,
                                data = result
                            };
                    }
                }

            }
            return null;
        }
    }
}