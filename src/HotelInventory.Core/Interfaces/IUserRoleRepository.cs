using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleSnapshot>> GetAllUserRolesAsync();
        Task<IEnumerable<UserRoleSnapshot>> GetFilteredUserRolesAsync(Expression<Func<UserRoleSnapshot, bool>> expression);
        Task CreateUserRole(UserRoleSnapshot obj);
        Task UpdateUserRole(UserRoleSnapshot obj);
        Task DeleteUserRole(UserRoleSnapshot obj);
    }
}
