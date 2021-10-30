using System.Collections.Generic;
using api.Repositories.Data;
using api.Repositories.Dependencies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.DBModels;

namespace api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            this._context = context;
        }

        public async Task<int> CreateCategory(Category category)
        {
            if (_context != null)
            {
                await _context.Category.AddAsync(category);
                await _context.SaveChangesAsync();
                return category.Id;
            }
            return 0;
        }

        public async Task<int> DeleteCategoryByID(int? id)
        {
            int result = 0;
            var dbitem = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (dbitem != null)
            {
                _context.Category.Remove(dbitem);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Category> GetCategoryByID(int id)
        {
            if (_context != null)
            {
                return await _context.Category.FirstOrDefaultAsync(item => item.Id == id);
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetListCategory()
        {
            if (_context != null)
            {
                return await _context.Category.ToListAsync();
            }
            return null;
        }

        public async Task UpdateCategory(Category category)
        {
            var dbitem = await _context.Category.FirstOrDefaultAsync(item => item.Id == category.Id);
            if (dbitem != null)
            {
                dbitem.Name = category.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}