using System.Collections.Generic;
using api.Repositories.Data;
using api.Repositories.Dependencies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.DBModels;
using System.Linq;
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
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            if (_context != null)
            {
                var item = await _context.Storage.FirstOrDefaultAsync(x => x.Id == id);
                _context.Storage.Remove(item);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<Storage>> Read()
        {
            if (_context != null)
            {
                return await _context.Storage.ToListAsync();
            }
            return null;
        }

        public async Task<int> Update(Storage data)
        {
            if (_context != null)
            {
                var dbitem = await _context.Storage.FirstOrDefaultAsync(item => item.Id == data.Id);
                if (dbitem != null)
                {
                    dbitem.Name = data.Name;
                    dbitem.Square=data.Square;
                    dbitem.ProvinceId=data.ProvinceId;
                    dbitem.DistrictId = data.DistrictId;
                    dbitem.VillageId=data.VillageId;
                    dbitem.Address=data.Address;
                    dbitem.Floor = data.Floor;
                    dbitem.WardId=data.WardId;
                   
                    await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
    }
}