using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Models;
namespace api.Repositories.Dependencies
{
    public interface IStorageRepository
    {
        Task<int> Create(Storage item);
        Task<IEnumerable<Storage>> Read();
        Task<int> Update(Storage item);
        Task<int> Delete(int id);
    }
}