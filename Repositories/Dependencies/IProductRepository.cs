using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface IProductRepository
    {
        Task<int> Create(ProductModel item);
        Task<IEnumerable<ProductModel>> GetProducts(int quantity);
        Task<System.Object> Search(string querySearch, int currentPage, int pageSize);
        Task<IEnumerable<ProductStatus>> GetStatuses();
        Task<IEnumerable<ProductPackingMethod>> GetPackMethods();
        Task<IEnumerable<Origin>> GetOrigins();
        Task<ProductModel> Detail(int productId);
    }
}