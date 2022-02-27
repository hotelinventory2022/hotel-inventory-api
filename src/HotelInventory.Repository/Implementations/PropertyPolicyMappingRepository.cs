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
    public class PropertyPolicyMappingRepository : GenericRepository<PropertyPolicyMappingSnapshot>, IPropertyPolicyMappingRepository
    {
        public PropertyPolicyMappingRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<PropertyPolicyMappingSnapshot>> GetAllPropertyPolicyMappingesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<PropertyPolicyMappingSnapshot>> GetFilteredPropertyPolicyMappingAsync(Expression<Func<PropertyPolicyMappingSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreatePropertyPolicyMapping(PropertyPolicyMappingSnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdatePropertyPolicyMapping(PropertyPolicyMappingSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeletePropertyPolicyMapping(PropertyPolicyMappingSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
