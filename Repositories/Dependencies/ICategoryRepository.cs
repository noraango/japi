using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;

namespace api.Repositories.Dependencies
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesByLevel(int level);
        Task<IEnumerable<Category>> GetSubcategoriesById(int id);
        Task<int> CreateCategory(Category category);
        Task<IEnumerable<Category>> GetListCategory();
        Task<Category> GetCategoryByID(int ID);
        Task UpdateCategory(Category category);
        Task<int> DeleteCategoryByID(int? ID);
    }
}