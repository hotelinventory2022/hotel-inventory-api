using HotelInventory.Models;
using HotelInventory.Models.PropertyFacilityMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IPropertyFacilityMappingService
    {
        Task<ApiResponse<IEnumerable<PropertyFacilityMappingDTO>>> GetAllPropertyFacilityMappingsAsync();
        Task<ApiResponse<IEnumerable<PropertyFacilityMappingDTO>>> GetFilteredPropertyFacilityMappingsAsyncByPropertyId(int PropertyId);
        Task<ApiResponse<PropertyFacilityMappingDTO>> CreatePropertyFacilityMapping(PropertyFacilityMappingDTO PropertyFacilityMapping);
        //Task<ApiResponse<PropertyFacilityMappingDTO>> UpdatePropertyFacilityMapping(PropertyFacilityMappingDTO PropertyFacilityMapping);
        Task<ApiResponse<bool>> DeletePropertyFacilityMapping(int PropertyFacilityMappingId);
    }
}
