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
    public class AvailabilityRepository : GenericRepository<AvailabiltyRateSnapshot>, IAvailabilityRepository
    {
        public AvailabilityRepository(DBContext _Context)
            : base(_Context)
        {
        }
        public async Task<IEnumerable<AvailabiltyRateSnapshot>> GetAllAvailabilityAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<AvailabiltyRateSnapshot>> GetFilteredAvailabilityAsync(Expression<Func<AvailabiltyRateSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateAvailability(IEnumerable<AvailabiltyRateSnapshot> obj)
        {
            await Create(obj);
        }
        public async Task UpdateAvailability(AvailabiltyRateSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteAvailability(AvailabiltyRateSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
