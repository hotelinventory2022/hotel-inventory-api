using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Repository.Implementations
{
    public class UserRoleRepository : GenericRepository<UserRoleSnapshot>, IUserRoleRepository
    {
        public UserRoleRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<UserRoleSnapshot>> GetAllUserRolesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<UserRoleSnapshot>> GetFilteredUserRolesAsync(Expression<Func<UserRoleSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateUserRole(UserRoleSnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdateUserRole(UserRoleSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteUserRole(UserRoleSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
