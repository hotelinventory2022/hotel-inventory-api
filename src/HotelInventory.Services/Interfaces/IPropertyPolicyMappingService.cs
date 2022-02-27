using HotelInventory.Models;
using HotelInventory.Models.PropertyPolicyMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IPropertyPolicyMappingService
    {
        Task<ApiResponse<IEnumerable<PropertyPolicyMappingDTO>>> GetAllPropertyPolicyMappingsAsync();
        Task<ApiResponse<IEnumerable<PropertyPolicyMappingDTO>>> GetFilteredPropertyPolicyMappingsAsyncByPropertyId(int PropertyId);
        Task<ApiResponse<PropertyPolicyMappingDTO>> CreatePropertyPolicyMapping(PropertyPolicyMappingDTO PropertyPolicyMapping);
        Task<ApiResponse<PropertyPolicyMappingDTO>> UpdatePropertyPolicyMapping(PropertyPolicyMappingDTO PropertyPolicyMapping);
        Task<ApiResponse<bool>> DeletePropertyPolicyMapping(int PropertyPolicyMappingId);
    }
}
