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
    public class RoomFacilityMappingRepository : GenericRepository<RoomFacilityMappingSnapshot>, IRoomFacilityMappingRepository
    {
        public RoomFacilityMappingRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<RoomFacilityMappingSnapshot>> GetAllRoomFacilityMappingesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<RoomFacilityMappingSnapshot>> GetFilteredRoomFacilityMappingAsync(Expression<Func<RoomFacilityMappingSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateRoomFacilityMapping(RoomFacilityMappingSnapshot obj)
        {
            await Create(obj);
        }
        public async Task DeleteRoomFacilityMapping(RoomFacilityMappingSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
