using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelInventory.Repository.Implementations
{
    public class RoleRepository : GenericRepository<RoleSnapshot>, IRoleRepository
    {
        public RoleRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<RoleSnapshot>> GetAllRolesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<RoleSnapshot>> GetFilteredRoleAsync(Expression<Func<RoleSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task<RoleSnapshot> GetRoleByIdAsync(int RoleId)
        {
            return await GetById(RoleId);
        }
        public async Task CreateRole(RoleSnapshot Role)
        {
            await Create(Role);
        }
        public async Task UpdateRole(RoleSnapshot Role)
        {
            await Update(Role);
        }
        public async Task DeleteRole(RoleSnapshot Role)
        {
            await Delete(Role);
        }
    }
}
