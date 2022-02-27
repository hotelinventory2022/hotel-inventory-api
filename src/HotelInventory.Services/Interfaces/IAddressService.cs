using HotelInventory.Models;
using HotelInventory.Models.Address;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IAddressService
    {
        Task<ApiResponse<IEnumerable<AddressDTO>>> GetAllAddresssAsync();
        Task<ApiResponse<AddressDTO>> CreateAddress(AddressDTO Address);
        Task<ApiResponse<AddressDTO>> UpdateAddress(AddressDTO Address);
        Task<ApiResponse<bool>> DeleteAddress(int AddressId);
    }
}
