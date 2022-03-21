using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<AvailabiltyRateSnapshot>> GetAllAvailabilityAsync();
        Task<IEnumerable<AvailabiltyRateSnapshot>> GetFilteredAvailabilityAsync(Expression<Func<AvailabiltyRateSnapshot, bool>> expression);
        Task CreateAvailability(IEnumerable<AvailabiltyRateSnapshot> obj);
        Task UpdateAvailability(AvailabiltyRateSnapshot obj);
        Task DeleteAvailability(AvailabiltyRateSnapshot obj);
    }
}
