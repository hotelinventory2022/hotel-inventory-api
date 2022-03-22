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
    public class AreaRepository : GenericRepository<Country_State_City_AreaSnapshot>, IAreaRepository
    {
        public AreaRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<Country_State_City_AreaSnapshot>> GetAllAreasAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<Country_State_City_AreaSnapshot>> GetFilteredAreasAsync(Expression<Func<Country_State_City_AreaSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        //public async Task CreateAddress(AddressSnapshot obj)
        //{
        //    await Create(obj);
        //}
        //public async Task UpdateAddress(AddressSnapshot obj)
        //{
        //    await Update(obj);
        //}
        //public async Task DeleteAddress(AddressSnapshot obj)
        //{
        //    await Delete(obj);
        //}
    }
}
