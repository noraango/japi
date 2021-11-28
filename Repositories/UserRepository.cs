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

        public async Task<object> Login(string username, string password)
        {
            if (_context != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(username) && x.EncodedPassword.Equals(password));
                if (user != null)
                {
                    var role = await _context.UserRole.FirstOrDefaultAsync(x => x.Id == user.UserRoleId);
                    var ward = await _context.LocationWard.FirstOrDefaultAsync(x => x.WardId.Equals(user.WardId));
                    var district = await _context.LocationDistrict.FirstOrDefaultAsync(x => x.DistrictId.Equals(user.DistrictId));
                    var province = await _context.LocationProvince.FirstOrDefaultAsync(x => x.ProvinceId.Equals(user.ProvinceId));
                    return new
                    {
                        status = true,
                        user = new
                        {
                            id = user.UserId,
                            avatar = user.AvatarFilename,
                            role = role.Name,
                            firstName = user.FirstName,
                            middleName = user.MiddleName,
                            lastName = user.LastName,
                            fullName = (user.LastName != null ? user.LastName + " " : "") + (user.MiddleName != null ? user.MiddleName + " " : "") + user.FirstName,
                            phone = user.Phone,
                            email = user.Email,
                            address = user.Address,
                            ward = ward.Name,
                            district = district.Name,
                            province = province.Name
                        }
                    };
                }
                else
                {
                    return new
                    {
                        status = false
                    };
                }
            }
            return null;
        }

        public async Task<object> Register(User data)
        {
            if (_context != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(data.Email));
                if (user == null)
                {
                    data.UserRoleId = 4;
                    await _context.User.AddAsync(data);
                    await _context.SaveChangesAsync();
                    var result = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(data.Email) && x.EncodedPassword.Equals(data.EncodedPassword));
                    if (result != null)
                    {
                        return new
                        {
                            status = true
                        };
                    }
                    else
                    {
                        return new
                        {
                            status = false
                        };
                    }
                }
                else
                {
                    return new
                    {
                        status = false
                    };
                }
            }
            return null;
        }
    }
}