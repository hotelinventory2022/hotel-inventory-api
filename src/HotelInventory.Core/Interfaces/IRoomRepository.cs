using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomSnapshot>> GetAllRoomesAsync();
        Task<IEnumerable<RoomSnapshot>> GetFilteredRoomAsync(Expression<Func<RoomSnapshot, bool>> expression);
        Task CreateRoom(RoomSnapshot obj);
        Task UpdateRoom(RoomSnapshot obj);
        Task DeleteRoom(RoomSnapshot obj);
    }
}
