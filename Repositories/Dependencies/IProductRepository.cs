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
        Task<System.Object> getComments(int productId, int currentPage, int pageSize);
        Task<System.Object> GetProductsByCategory(int categoryId, int currentPage, int pageSize);

        Task<List<string>> DetailImages(int productId);

        Task<System.Object> GetComment(int productId, int page, int size);
       Task<System.Object>  getCategory(int categoryId,int page,int size);

      Task<System.Object>  ShopProduct(int userId,string querySearch, int currentPage, int pageSize);

      Task<System.Object> addComment(int userId, int productId, int rating,string comment);
      Task<System.Object> SearchFilter(string querySearch, int price, int rate, int currentPage, int pageSize);

      Task<System.Object> getCategoryFilter(int categoryId, int price, int rate, int page, int size);
    }
}