using HotelInventory.Models;
using HotelInventory.Models.PropertyImageMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IPropertyImageMappingService
    {
        Task<ApiResponse<IEnumerable<PropertyImageMappingDTO>>> GetAllPropertyImageMappingsAsync();
        Task<ApiResponse<IEnumerable<PropertyImageMappingDTO>>> GetFilteredPropertyImageMappingsAsyncByPropertyId(int PropertyId);
        Task<ApiResponse<PropertyImageMappingDTO>> CreatePropertyImageMapping(PropertyImageMappingDTO PropertyImageMapping);
        //Task<ApiResponse<PropertyImageMappingDTO>> UpdatePropertyImageMapping(PropertyImageMappingDTO PropertyImageMapping);
        Task<ApiResponse<bool>> DeletePropertyImageMapping(int PropertyImageMappingId);
    }
}
