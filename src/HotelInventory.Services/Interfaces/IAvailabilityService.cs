using HotelInventory.Models;
using HotelInventory.Models.Room;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IAvailabilityService
    {
        Task<ApiResponse<IEnumerable<AvailabiltyDTO>>> GetAvailibiltyAsync(AvailabiltyRequestModel availabilty);
        Task<ApiResponse<AvailabiltyDTO>> CreateOrUpdateAvailibilty(AvailabiltyRequestModel Availibilty);
        //Task<ApiResponse<AvailabiltyDTO>> UpdateAvailibilty(AvailabiltyRequestModel Availibilty);
        Task<ApiResponse<bool>> DeleteAvailibilty(int AvailibiltyId);
    }
}
