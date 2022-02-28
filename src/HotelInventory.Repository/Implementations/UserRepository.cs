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
    public class UserRepository : GenericRepository<UserSnapshot>, IUserRepository
    {
        public UserRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<UserSnapshot>> GetAllUseresAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<UserSnapshot>> GetFilteredUserAsync(Expression<Func<UserSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateUser(UserSnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdateUser(UserSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteUser(UserSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
