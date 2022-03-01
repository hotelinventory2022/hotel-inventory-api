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
    public class AddressRepository : GenericRepository<AddressSnapshot>, IAddressRepository
    {
        public AddressRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<AddressSnapshot>> GetAllAddressesAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<AddressSnapshot>> GetFilteredAddressAsync(Expression<Func<AddressSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateAddress(AddressSnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdateAddress(AddressSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteAddress(AddressSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
