using HotelInventory.Models;
using HotelInventory.Models.RoomFacilityMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IRoomFacilityMappingService
    {
        Task<ApiResponse<IEnumerable<RoomFacilityMappingDTO>>> GetAllRoomFacilityMappingsAsync();
        Task<ApiResponse<IEnumerable<RoomFacilityMappingDTO>>> GetFilteredRoomFacilityMappingsAsyncByRoomId(int RoomId);
        Task<ApiResponse<RoomFacilityMappingDTO>> CreateRoomFacilityMapping(RoomFacilityMappingDTO RoomFacilityMapping);
        //Task<ApiResponse<RoomFacilityMappingDTO>> UpdateRoomFacilityMapping(RoomFacilityMappingDTO RoomFacilityMapping);
        Task<ApiResponse<bool>> DeleteRoomFacilityMapping(int RoomFacilityMappingId);
    }
}
