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
    public class PropertyImageMappingRepository : GenericRepository<PropertyImageMappingSnapshot>, IPropertyImageMappingRepository
    {
        public PropertyImageMappingRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<PropertyImageMappingSnapshot>> GetAllPropertyImageMappingesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<PropertyImageMappingSnapshot>> GetFilteredPropertyImageMappingAsync(Expression<Func<PropertyImageMappingSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreatePropertyImageMapping(PropertyImageMappingSnapshot obj)
        {
            await Create(obj);
        }
        public async Task DeletePropertyImageMapping(PropertyImageMappingSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
