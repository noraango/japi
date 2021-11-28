using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.DBModels;
namespace api.Repositories.Dependencies
{
    public interface IUserRepository
    {
        Task<object> Login(string username, string password);
        Task<object> Register(User data);
        Task<int> Forget(int id);
    }
}