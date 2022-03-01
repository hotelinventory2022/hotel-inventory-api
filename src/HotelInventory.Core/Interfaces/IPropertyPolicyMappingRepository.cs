using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IPropertyPolicyMappingRepository
    {
        Task<IEnumerable<PropertyPolicyMappingSnapshot>> GetAllPropertyPolicyMappingesAsync();
        Task<IEnumerable<PropertyPolicyMappingSnapshot>> GetFilteredPropertyPolicyMappingAsync(Expression<Func<PropertyPolicyMappingSnapshot, bool>> expression);
        Task CreatePropertyPolicyMapping(PropertyPolicyMappingSnapshot obj);
        Task UpdatePropertyPolicyMapping(PropertyPolicyMappingSnapshot obj);
        Task DeletePropertyPolicyMapping(PropertyPolicyMappingSnapshot obj);
    }
}
