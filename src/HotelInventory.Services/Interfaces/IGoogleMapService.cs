using HotelInventory.Models;
using HotelInventory.Models.GoogleMapDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    class IGoogleMapService
    {
    }
    public interface IGoogleMapDetailsService
    {
        Task<ApiResponse<IEnumerable<GoogleMapDetailsDto>>> GetAllGoogleMapDetailssAsync();
        Task<ApiResponse<GoogleMapDetailsDto>> CreateGoogleMapDetails(GoogleMapDetailsDto GoogleMapDetails);
        Task<ApiResponse<GoogleMapDetailsDto>> UpdateGoogleMapDetails(GoogleMapDetailsDto GoogleMapDetails);
        Task<ApiResponse<bool>> DeleteGoogleMapDetails(int GoogleMapDetailsId);
    }
}
