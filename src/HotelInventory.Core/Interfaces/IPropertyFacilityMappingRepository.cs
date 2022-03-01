using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IPropertyFacilityMappingRepository
    {
        Task<IEnumerable<PropertyFacilityMappingSnapshot>> GetAllPropertyFacilityMappingesAsync();
        Task<IEnumerable<PropertyFacilityMappingSnapshot>> GetFilteredPropertyFacilityMappingAsync(Expression<Func<PropertyFacilityMappingSnapshot, bool>> expression);
        Task CreatePropertyFacilityMapping(PropertyFacilityMappingSnapshot obj);
        Task DeletePropertyFacilityMapping(PropertyFacilityMappingSnapshot obj);
    }
}
