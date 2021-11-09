using System.Collections.Generic;
using api.Repositories.Data;
using api.Repositories.Dependencies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.DBModels;
using System.Linq;
namespace api.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly Context _context;

        public StoreRepository(Context context)
        {
            this._context = context;
        }

        public async Task<int> Create(Store item)
        {
            if (_context != null)
            {
                await _context.Store.AddAsync(item);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            if (_context != null)
            {
                var item = await _context.Store.FirstOrDefaultAsync(x => x.Id == id);
                _context.Store.Remove(item);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<Store>> Read()
        {
            if (_context != null)
            {
                return await _context.Store.ToListAsync();
            }
            return null;
        }

        public async Task<int> Update(Store data)
        {
            if (_context != null)
            {
                var dbitem = await _context.Store.FirstOrDefaultAsync(item => item.Id == data.Id);
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