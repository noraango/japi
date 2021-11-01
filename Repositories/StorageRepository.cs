using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using api.Repositories.Data;
namespace api.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly Context _context;

        public StorageRepository(Context context)
        {
            this._context = context;
        }

        public async Task<int> Create(Storage item)
        {
            if (_context != null)
            {
                await _context.Storage.AddAsync(item);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public Task<int> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Storage>> Read()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Storage item)
        {
            throw new System.NotImplementedException();
        }
    }
}