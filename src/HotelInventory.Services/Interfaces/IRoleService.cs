using HotelInventory.Models;
using HotelInventory.Models.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponse<IEnumerable<RoleDto>>> GetAllRolesAsync();
        Task<ApiResponse<RoleDto>> GetRoleByNameAsync(string roleName);
        Task<ApiResponse<RoleDto>> GetRoleByIdAsync(int RoleId);
        Task<ApiResponse<RoleDto>> CreateRole(RoleDto Role);
        Task<ApiResponse<RoleDto>> UpdateRole(RoleDto Role);
        Task<ApiResponse<bool>> DeleteRole(int RoleId);
    }
}
