using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressSnapshot>> GetAllAddressesAsync();
        Task<IEnumerable<AddressSnapshot>> GetFilteredAddressAsync(Expression<Func<AddressSnapshot, bool>> expression);
        Task CreateAddress(AddressSnapshot obj);
        Task UpdateAddress(AddressSnapshot obj);
        Task DeleteAddress(AddressSnapshot obj);
    }
}
