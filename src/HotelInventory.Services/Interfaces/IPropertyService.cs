using HotelInventory.Models;
using HotelInventory.Models.Property;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<ApiResponse<IEnumerable<PropertyDTO>>> GetAllPropertysAsync();
        Task<ApiResponse<IEnumerable<PropertyDTO>>> GetFilteredPropertysAsyncByPropertyId(int PropertyId);
        Task<ApiResponse<PropertyDTO>> CreateProperty(PropertyDTO Property);
        Task<ApiResponse<PropertyDTO>> UpdateProperty(PropertyDTO Property);
        Task<ApiResponse<bool>> DeleteProperty(int PropertyId);
        Task<ApiResponse<bool>> UploadPropertyByOwner(PropertyUploadRequestModel model);
    }
}
