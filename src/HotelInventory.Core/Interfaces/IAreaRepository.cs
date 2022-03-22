using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Country_State_City_AreaSnapshot>> GetAllAreasAsync();
        Task<IEnumerable<Country_State_City_AreaSnapshot>> GetFilteredAreasAsync(Expression<Func<Country_State_City_AreaSnapshot, bool>> expression);
        //Task CreateAddress(AddressSnapshot obj);
        //Task UpdateAddress(AddressSnapshot obj);
        //Task DeleteAddress(AddressSnapshot obj);
    }
}
