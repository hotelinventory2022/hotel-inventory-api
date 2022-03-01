using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserSnapshot>> GetAllUseresAsync();
        Task<IEnumerable<UserSnapshot>> GetFilteredUserAsync(Expression<Func<UserSnapshot, bool>> expression);
        Task CreateUser(UserSnapshot obj);
        Task UpdateUser(UserSnapshot obj);
        Task DeleteUser(UserSnapshot obj);
    }
}
