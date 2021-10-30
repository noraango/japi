using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;

namespace api.Repositories.Dependencies
{
    public interface ICategoryRepository
    {
        Task<int> CreateCategory(Category category);
        Task<IEnumerable<Category>> GetListCategory();
        Task<Category> GetCategoryByID(int ID);
        Task UpdateCategory(Category category);
        Task<int> DeleteCategoryByID(int? ID);
    }
}