using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<PropertySnapshot>> GetAllPropertyesAsync();
        Task<IEnumerable<PropertySnapshot>> GetFilteredPropertyAsync(Expression<Func<PropertySnapshot, bool>> expression);
        Task CreateProperty(PropertySnapshot obj);
        Task UpdateProperty(PropertySnapshot obj);
        Task DeleteProperty(PropertySnapshot obj);
    }
}
