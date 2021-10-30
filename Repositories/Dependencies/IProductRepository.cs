using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
namespace api.Repositories.Dependencies
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int quantity);
        Task<IEnumerable<Product>> Search(string querySearch, int currentPage, int pageSize);
    }
}