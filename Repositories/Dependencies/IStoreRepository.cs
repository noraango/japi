using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface IStoreRepository
    {
        Task<int> Create(Store item);
        Task<IEnumerable<Store>> Read();
        Task<int> Update(Store data);
        Task<int> Delete(int id);
        Task<int> DeleteItem(int id);
        Task<System.Object> Detail(int id, int page, int size);
    }
}