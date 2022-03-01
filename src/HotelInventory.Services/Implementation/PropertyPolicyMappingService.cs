using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.PropertyPolicyMapping;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class PropertyPolicyMappingService : IPropertyPolicyMappingService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IPropertyPolicyMappingRepository _repo;
        public PropertyPolicyMappingService(ILoggerManager logger, IMapper mapper, IPropertyPolicyMappingRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<PropertyPolicyMappingDTO>>> GetAllPropertyPolicyMappingsAsync()
        {
            try
            {
                var PropertyPolicyMappinges = await _repo.GetAllPropertyPolicyMappingesAsync();
                _logger.LogInfo($"Returned all PropertyPolicyMappinges from database.");

                var PropertyPolicyMappingResult = _mapper.Map<IEnumerable<PropertyPolicyMappingDTO>>(PropertyPolicyMappinges);
                return new ApiResponse<IEnumerable<PropertyPolicyMappingDTO>> { Data = PropertyPolicyMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all PropertyPolicyMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPropertyPolicyMappingsAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyPolicyMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<IEnumerable<PropertyPolicyMappingDTO>>> GetFilteredPropertyPolicyMappingsAsyncByPropertyId(int PropertyId)
        {
            try
            {
                Expression<Func<PropertyPolicyMappingSnapshot, bool>> filter = _ => _.PropertyId == PropertyId;
                var PropertyPolicyMappinges = await _repo.GetFilteredPropertyPolicyMappingAsync(filter);
                _logger.LogInfo($"Returned filtered PropertyPolicyMappinges from database.");

                var PropertyPolicyMappingResult = _mapper.Map<IEnumerable<PropertyPolicyMappingDTO>>(PropertyPolicyMappinges);
                return new ApiResponse<IEnumerable<PropertyPolicyMappingDTO>> { Data = PropertyPolicyMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned filtered PropertyPolicyMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredPropertyPolicyMappingsAsyncByPropertyId action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyPolicyMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<PropertyPolicyMappingDTO>> CreatePropertyPolicyMapping(PropertyPolicyMappingDTO propertyPolicyMappingObj)
        {
            PropertyPolicyMappingDTO createdObj = null;
            try
            {
                if (propertyPolicyMappingObj == null)
                {
                    _logger.LogError("PropertyPolicyMapping object sent from client is null.");
                    return new ApiResponse<PropertyPolicyMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "PropertyPolicyMapping object sent from client is null" };
                }
                Expression<Func<PropertyPolicyMappingSnapshot, bool>> filter = _ => _.PropertyId == propertyPolicyMappingObj.PropertyId;
                var PropertyPolicyMapping = await _repo.GetFilteredPropertyPolicyMappingAsync(filter);
                if (PropertyPolicyMapping.Count() == 0)
                {
                    var PropertyPolicyMappingEntity = _mapper.Map<PropertyPolicyMappingSnapshot>(PropertyPolicyMapping);
                    await _repo.CreatePropertyPolicyMapping(PropertyPolicyMappingEntity);
                    createdObj = _mapper.Map<PropertyPolicyMappingDTO>(PropertyPolicyMappingEntity);
                    _logger.LogInfo($"Succesfully created PropertyPolicyMapping with id {PropertyPolicyMappingEntity.Id.ToString()}.");
                    return new ApiResponse<PropertyPolicyMappingDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {PropertyPolicyMappingEntity.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"PropertyPolicyMapping already exists for the property with id - {PropertyPolicyMapping.FirstOrDefault().PropertyId.ToString()}.");
                    return new ApiResponse<PropertyPolicyMappingDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"PropertyPolicyMapping already exists for the property with id - {PropertyPolicyMapping.FirstOrDefault().PropertyId.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePropertyPolicyMapping action: {ex.Message}");
                return new ApiResponse<PropertyPolicyMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<PropertyPolicyMappingDTO>> UpdatePropertyPolicyMapping(PropertyPolicyMappingDTO PropertyPolicyMapping)
        {
            PropertyPolicyMappingDTO updatedobj = null;
            try
            {
                if (PropertyPolicyMapping == null)
                {
                    _logger.LogError("PropertyPolicyMapping object sent from client is null.");
                    return new ApiResponse<PropertyPolicyMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "PropertyPolicyMapping object sent from client is null" };
                }

                var PropertyPolicyMappingEntity = _mapper.Map<PropertyPolicyMappingSnapshot>(PropertyPolicyMapping);

                await _repo.UpdatePropertyPolicyMapping(PropertyPolicyMappingEntity);
                updatedobj = _mapper.Map<PropertyPolicyMappingDTO>(PropertyPolicyMappingEntity);

                _logger.LogInfo($"Succesfully updated PropertyPolicyMapping object with id {PropertyPolicyMappingEntity.Id.ToString()}.");
                return new ApiResponse<PropertyPolicyMappingDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated PropertyPolicyMapping object with id {PropertyPolicyMappingEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePropertyPolicyMapping action: {ex.Message}");
                return new ApiResponse<PropertyPolicyMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeletePropertyPolicyMapping(int PropertyPolicyMappingId)
        {
            try
            {
                Expression<Func<PropertyPolicyMappingSnapshot, bool>> filter = _ => _.Id == PropertyPolicyMappingId;
                var existingObj = await _repo.GetFilteredPropertyPolicyMappingAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"PropertyPolicyMapping with id: {PropertyPolicyMappingId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"PropertyPolicyMapping with id: {PropertyPolicyMappingId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned PropertyPolicyMapping with id: {PropertyPolicyMappingId}");
                    var PropertyPolicyMappingEntity = _mapper.Map<PropertyPolicyMappingSnapshot>(existingObj);
                    await _repo.DeletePropertyPolicyMapping(PropertyPolicyMappingEntity);
                    _logger.LogInfo($"Succesfully deleted PropertyPolicyMapping object with id {PropertyPolicyMappingEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted PropertyPolicyMapping with id {PropertyPolicyMappingEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePropertyPolicyMapping action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
