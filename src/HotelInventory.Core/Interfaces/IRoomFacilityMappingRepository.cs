using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IRoomFacilityMappingRepository
    {
        Task<IEnumerable<RoomFacilityMappingSnapshot>> GetAllRoomFacilityMappingesAsync();
        Task<IEnumerable<RoomFacilityMappingSnapshot>> GetFilteredRoomFacilityMappingAsync(Expression<Func<RoomFacilityMappingSnapshot, bool>> expression);
        Task CreateRoomFacilityMapping(RoomFacilityMappingSnapshot obj);
        Task DeleteRoomFacilityMapping(RoomFacilityMappingSnapshot obj);
    }
}
