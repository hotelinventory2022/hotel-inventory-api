using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleSnapshot>> GetAllRolesAsync();
        Task<RoleSnapshot> GetRoleByIdAsync(int RoleId);
        Task<IEnumerable<RoleSnapshot>> GetFilteredRoleAsync(Expression<Func<RoleSnapshot, bool>> expression);
        Task CreateRole(RoleSnapshot Role);
        Task UpdateRole(RoleSnapshot Role);
        Task DeleteRole(RoleSnapshot Role);
    }
}
