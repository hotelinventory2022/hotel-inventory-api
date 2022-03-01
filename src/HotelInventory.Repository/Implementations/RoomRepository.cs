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
    public class RoomRepository : GenericRepository<RoomSnapshot>, IRoomRepository
    {
        public RoomRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<RoomSnapshot>> GetAllRoomesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<RoomSnapshot>> GetFilteredRoomAsync(Expression<Func<RoomSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateRoom(RoomSnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdateRoom(RoomSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteRoom(RoomSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
