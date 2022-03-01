using HotelInventory.Models;
using HotelInventory.Models.Room;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IRoomService
    {
        Task<ApiResponse<IEnumerable<RoomDTO>>> GetAllRoomsAsync();
        Task<ApiResponse<IEnumerable<RoomDTO>>> GetFilteredRoomsAsyncByRoomId(int RoomId);
        Task<ApiResponse<RoomDTO>> CreateRoom(RoomDTO Room);
        Task<ApiResponse<RoomDTO>> UpdateRoom(RoomDTO Room);
        Task<ApiResponse<bool>> DeleteRoom(int RoomId);
    }
}
