using HotelInventory.Models;
using HotelInventory.Models.Lookup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface ILookupDetailsService
    {
        Task<ApiResponse<IEnumerable<LookupDetailsDTO>>> GetAllLookupDetailssAsync();
        Task<ApiResponse<LookupDetailsDTO>> GetLookupDetailsByTypeAsync(string LookupType);
    }
}
