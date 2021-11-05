using System.Collections.Generic;
using api.Repositories.Data;
using api.Repositories.Dependencies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.DBModels;
using System.Linq;

namespace api.Repositories.Dependencies
{
    public class LocationRepository : ILocationRepository
    {
        private readonly Context _context;
        public LocationRepository(Context context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<LocationProvince>> GetProvince()
        {
            if (_context != null)
            {
                var result = await _context.LocationProvince.ToListAsync();
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<LocationDistrict>> GetDistrict(string provinceId)
        {
            if (_context != null)
            {               
                var result = await _context.LocationDistrict.AsQueryable().Where(x => x.ProvinceId.Equals(provinceId)).ToListAsync();
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<LocationWard>> GetWard(string districtId)
        {
            if (_context != null)
            {
              
                var result = await _context.LocationWard.AsQueryable().Where(x => x.DistrictId.Equals(districtId)).ToListAsync();
                return result;
            }
            return null;
        }
    }
}