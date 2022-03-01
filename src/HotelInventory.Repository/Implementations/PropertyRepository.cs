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
    public class PropertyRepository : GenericRepository<PropertySnapshot>, IPropertyRepository
    {
        public PropertyRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<PropertySnapshot>> GetAllPropertyesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<PropertySnapshot>> GetFilteredPropertyAsync(Expression<Func<PropertySnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateProperty(PropertySnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdateProperty(PropertySnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteProperty(PropertySnapshot obj)
        {
            await Delete(obj);
        }
    }
}
