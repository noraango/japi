using System.Collections.Generic;
using api.Repositories.Data;
using api.Repositories.Dependencies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.DBModels;
using System.Linq;
namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            this._context = context;
        }

        public Task<int> Forget(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> Login(string phone, string password)
        {
            if (_context != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Phone.Equals(phone) && x.EncodedPassword.Equals(password));
                if (user != null)
                {
                    var role = await _context.UserRole.FirstOrDefaultAsync(x => x.Id == user.UserRoleId);
                    var ward = await _context.LocationWard.FirstOrDefaultAsync(x => x.WardId.Equals(user.WardId));
                    var district = await _context.LocationDistrict.FirstOrDefaultAsync(x => x.DistrictId.Equals(user.DistrictId));
                    var province = await _context.LocationProvince.FirstOrDefaultAsync(x => x.ProvinceId.Equals(user.ProvinceId));
                    return new
                    {
                        id = user.UserId,
                        avatar = user.AvatarURL,
                        role = role.Name,
                        firstName = user.FirstName,
                        middleName = user.MiddleName,
                        lastName = user.LastName,
                        phone = user.Phone,
                        email = user.Email,
                        address = user.Address,
                        ward = ward.Name,
                        district = district.Name,
                        province = province.Name
                    };
                }
            }
            return null;
        }

        public Task<int> Register(User data)
        {
            throw new System.NotImplementedException();
        }
    }
}