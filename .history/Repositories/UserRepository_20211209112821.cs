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

        public async Task<object> FilterUser(int currentPage, int pageSize, int roleId, int status)
        {

            if (roleId > 1)
            {
                if (status == 99)
                {
                    var totalRow = await _context.User.Where(x => x.UserRoleId == roleId).CountAsync();
                    var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                    var result = await _context.User.Where(x => x.UserRoleId == roleId).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.UserId).ToListAsync();
                    return new
                    {
                        totalPage = totalPage,
                        totalRow = totalRow,
                        data = result
                    };
                }
                else
                {
                    var totalRow = await _context.User.Where(x => x.UserRoleId == roleId && x.Status == status).CountAsync();
                    var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                    var result = await _context.User.Where(x => x.UserRoleId == roleId && x.Status == status).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.UserId).ToListAsync();
                    return new
                    {
                        totalPage = totalPage,
                        totalRow = totalRow,
                        data = result
                    };
                }
            }
            else
            {
                if (status == 99)
                {
                    var totalRow = await _context.User.CountAsync();
                    var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                    var result = await _context.User.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.UserId).ToListAsync();
                    return new
                    {
                        totalPage = totalPage,
                        totalRow = totalRow,
                        data = result
                    };
                }
                else
                {
                    var totalRow = await _context.User.Where(x => x.Status == status).CountAsync();
                    var totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;
                    var result = await _context.User.Where(x => x.Status == status).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.UserId).ToListAsync();
                    return new
                    {
                        totalPage = totalPage,
                        totalRow = totalRow,
                        data = result
                    };
                };
            }

        }

        public Task<int> Forget(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> GetRequest(int currentPage, int pageSize)
        {
            var totalRequest = await _context.RoleRequest.Where(x => x.status == 0).CountAsync();
            var totalPage = (totalRequest % pageSize == 0) ? (totalRequest / pageSize) : (totalRequest / pageSize) + 1;
            var source = await _context.RoleRequest.Where(x => x.status == 0).Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();
            List<RoleRequestAdmin> result = new List<RoleRequestAdmin>();
            foreach (var item in source)
            {
                if (item != null)
                {
                    var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                    var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == item.DistrictID.Trim()).FirstOrDefaultAsync();
                    var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == item.ProvinceID.Trim()).FirstOrDefaultAsync();
                    result.Add(new RoleRequestAdmin()
                    {
                        District = district.Name,
                        Province = city.Name,
                        infor = user,
                        request = item
                    });
                }
            }
            return new
            {
                totalPage = totalPage,
                totalRequest = totalRequest,
                data = result
            };
        }



        public async Task<object> Login(string username, string password)
        {
            if (_context != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(username) && x.EncodedPassword.Equals(password) && x.Status == 1);
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
                            province = province.Name,
                            districtId = district.DistrictId,
                            provinceId = province.ProvinceId
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
        public async Task<object> CheckEmail(string email)
        {
            if (_context != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(email));
                if (user == null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<object> Register(User data)
        {
            if (_context != null)
            {

                data.UserRoleId = 4;
                data.Status = 1;
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
            return null;
        }

        public async Task<object> RoleRegister(RoleRequest request)
        {
            if (_context != null)
            {
                if (request != null)
                {
                    await _context.RoleRequest.AddAsync(request);
                    await _context.SaveChangesAsync();
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
            return null;
        }

        public async Task<object> UpdateRoleRequest(RoleRequest request)
        {
            if (_context != null)
            {
                var user = await _context.RoleRequest.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (request != null && user != null)
                {
                    user.status = request.status;
                    if (user.status == 1)
                    {
                        var userdata = await _context.User.FirstOrDefaultAsync(x => x.UserId == user.UserId);
                        if (user.Role.Trim() == "U5")
                        {
                            userdata.UserRoleId = 5;
                        }
                        else
                        {
                            userdata.UserRoleId = 2;
                        }

                    }
                    await _context.SaveChangesAsync();
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
            return null;
        }

        public async Task<object> UpdateUser(User user)
        {
            if (_context != null)
            {
                var data = await _context.User.FirstOrDefaultAsync(x => x.UserId == user.UserId);
                if (data != null)
                {
                    data.Status = user.Status;
                    await _context.SaveChangesAsync();
                    return new
                    {
                        status = true
                    };
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return new
                    {
                        status = false
                    };
                }
            }
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<object> viewRole(int userId)
        {
            if (_context != null)
            {
                var item = await _context.RoleRequest.FirstOrDefaultAsync(x => x.UserId == userId);
                var district = await _context.Set<LocationDistrict>().Where(x => x.DistrictId.Trim() == item.DistrictID.Trim()).FirstOrDefaultAsync();
                var city = await _context.Set<LocationProvince>().Where(x => x.ProvinceId.Trim() == item.ProvinceID.Trim()).FirstOrDefaultAsync();
                return new
                {
                    district = district.Name,
                    city = city.Name,
                    CMTcode = item.CMTCode
                };
            }
            return null;

        }
    }
}