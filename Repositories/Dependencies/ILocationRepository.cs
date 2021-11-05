using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface ILocationRepository
    {
        Task<IEnumerable<LocationProvince>> GetProvince();
        Task<IEnumerable<LocationDistrict>> GetDistrict(string provinceId);
        Task<IEnumerable<LocationWard>> GetWard(string districtId);

    }
}