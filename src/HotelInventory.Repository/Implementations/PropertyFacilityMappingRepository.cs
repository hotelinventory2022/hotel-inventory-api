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
    public class PropertyFacilityMappingRepository : GenericRepository<PropertyFacilityMappingSnapshot>, IPropertyFacilityMappingRepository
    {
        public PropertyFacilityMappingRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<PropertyFacilityMappingSnapshot>> GetAllPropertyFacilityMappingesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<PropertyFacilityMappingSnapshot>> GetFilteredPropertyFacilityMappingAsync(Expression<Func<PropertyFacilityMappingSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreatePropertyFacilityMapping(PropertyFacilityMappingSnapshot obj)
        {
            await Create(obj);
        }
        public async Task DeletePropertyFacilityMapping(PropertyFacilityMappingSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
