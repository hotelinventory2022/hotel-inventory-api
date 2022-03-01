using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IPropertyImageMappingRepository
    {
        Task<IEnumerable<PropertyImageMappingSnapshot>> GetAllPropertyImageMappingesAsync();
        Task<IEnumerable<PropertyImageMappingSnapshot>> GetFilteredPropertyImageMappingAsync(Expression<Func<PropertyImageMappingSnapshot, bool>> expression);
        Task CreatePropertyImageMapping(PropertyImageMappingSnapshot obj);
        Task DeletePropertyImageMapping(PropertyImageMappingSnapshot obj);
    }
}
