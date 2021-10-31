using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProducts(int quantity);
        Task<IEnumerable<ProductModel>> Search(string querySearch, int currentPage, int pageSize);
    }
}