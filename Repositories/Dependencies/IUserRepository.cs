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

        Task<object> RoleRegister(RoleRequest request);
        Task<object> UpdateRoleRequest(RoleRequest request);
        Task<System.Object> GetRequest(int currentPage, int pageSize);
        Task<System.Object> FilterUser(int currentPage, int pageSize,int roleId ,int status);

        Task<object> UpdateUser(User request);

         Task<object> viewRole(int userId);
    }
}