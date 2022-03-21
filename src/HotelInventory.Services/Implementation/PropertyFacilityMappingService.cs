using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.PropertyFacilityMapping;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class PropertyFacilityMappingService : IPropertyFacilityMappingService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IPropertyFacilityMappingRepository _repo;
        public PropertyFacilityMappingService(ILoggerManager logger, IMapper mapper, IPropertyFacilityMappingRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<PropertyFacilityMappingDTO>>> GetAllPropertyFacilityMappingsAsync()
        {
            try
            {
                var PropertyFacilityMappinges = await _repo.GetAllPropertyFacilityMappingesAsync();
                _logger.LogInfo($"Returned all PropertyFacilityMappinges from database.");

                var PropertyFacilityMappingResult = _mapper.Map<IEnumerable<PropertyFacilityMappingDTO>>(PropertyFacilityMappinges);
                return new ApiResponse<IEnumerable<PropertyFacilityMappingDTO>> { Data = PropertyFacilityMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all PropertyFacilityMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPropertyFacilityMappingsAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyFacilityMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<IEnumerable<PropertyFacilityMappingDTO>>> GetFilteredPropertyFacilityMappingsAsyncByPropertyId(int PropertyId)
        {
            try
            {
                Expression<Func<PropertyFacilityMappingSnapshot, bool>> filter = _ => _.PropertyId == PropertyId;
                var PropertyFacilityMappinges = await _repo.GetFilteredPropertyFacilityMappingAsync(filter);
                _logger.LogInfo($"Returned filtered PropertyFacilityMappinges from database.");

                var PropertyFacilityMappingResult = _mapper.Map<IEnumerable<PropertyFacilityMappingDTO>>(PropertyFacilityMappinges);
                return new ApiResponse<IEnumerable<PropertyFacilityMappingDTO>> { Data = PropertyFacilityMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned filtered PropertyFacilityMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredPropertyFacilityMappingsAsyncByPropertyId action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyFacilityMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<PropertyFacilityMappingDTO>> CreatePropertyFacilityMapping(PropertyFacilityMappingDTO propertyFacilityMappingObj)
        {
            PropertyFacilityMappingDTO createdObj = null;
            try
            {
                if (propertyFacilityMappingObj == null)
                {
                    _logger.LogError("PropertyFacilityMapping object sent from client is null.");
                    return new ApiResponse<PropertyFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "PropertyFacilityMapping object sent from client is null" };
                }
                Expression<Func<PropertyFacilityMappingSnapshot, bool>> filter = _ => _.PropertyId == propertyFacilityMappingObj.PropertyId && _.FaciltiyId == propertyFacilityMappingObj.FaciltiyId;
                var PropertyFacilityMapping = await _repo.GetFilteredPropertyFacilityMappingAsync(filter);
                if (PropertyFacilityMapping.Count() == 0)
                {
                    var PropertyFacilityMappingEntity = _mapper.Map<PropertyFacilityMappingSnapshot>(propertyFacilityMappingObj);
                    await _repo.CreatePropertyFacilityMapping(PropertyFacilityMappingEntity);
                    createdObj = _mapper.Map<PropertyFacilityMappingDTO>(PropertyFacilityMappingEntity);
                    _logger.LogInfo($"Succesfully created PropertyFacilityMapping with id {PropertyFacilityMappingEntity.Id.ToString()}.");
                    return new ApiResponse<PropertyFacilityMappingDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {PropertyFacilityMappingEntity.Id.ToString()}." };
                }
                else
                {
                    var existingObj = _mapper.Map<PropertyFacilityMappingDTO>(PropertyFacilityMapping.FirstOrDefault());
                    _logger.LogError($"PropertyFacilityMapping already exists with Facility - {PropertyFacilityMapping.FirstOrDefault().FaciltiyId.ToString()}.");
                    return new ApiResponse<PropertyFacilityMappingDTO> { Data = existingObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"PropertyFacilityMapping already exists with Facility - {PropertyFacilityMapping.FirstOrDefault().FaciltiyId.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePropertyFacilityMapping action: {ex.Message}");
                return new ApiResponse<PropertyFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        //public async Task<ApiResponse<PropertyFacilityMappingDTO>> UpdatePropertyFacilityMapping(PropertyFacilityMappingDTO PropertyFacilityMapping)
        //{
        //    PropertyFacilityMappingDTO updatedobj = null;
        //    try
        //    {
        //        if (PropertyFacilityMapping == null)
        //        {
        //            _logger.LogError("PropertyFacilityMapping object sent from client is null.");
        //            return new ApiResponse<PropertyFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "PropertyFacilityMapping object sent from client is null" };
        //        }

        //        var PropertyFacilityMappingEntity = _mapper.Map<PropertyFacilityMappingSnapshot>(PropertyFacilityMapping);

        //        await _repo.UpdatePropertyFacilityMapping(PropertyFacilityMappingEntity);
        //        updatedobj = _mapper.Map<PropertyFacilityMappingDTO>(PropertyFacilityMappingEntity);

        //        _logger.LogInfo($"Succesfully updated PropertyFacilityMapping object with id {PropertyFacilityMappingEntity.Id.ToString()}.");
        //        return new ApiResponse<PropertyFacilityMappingDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated PropertyFacilityMapping object with id {PropertyFacilityMappingEntity.Id.ToString()}." };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside UpdatePropertyFacilityMapping action: {ex.Message}");
        //        return new ApiResponse<PropertyFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
        //    }
        //}
        public async Task<ApiResponse<bool>> DeletePropertyFacilityMapping(int PropertyFacilityMappingId)
        {
            try
            {
                Expression<Func<PropertyFacilityMappingSnapshot, bool>> filter = _ => _.Id == PropertyFacilityMappingId;
                var existingObj = await _repo.GetFilteredPropertyFacilityMappingAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"PropertyFacilityMapping with id: {PropertyFacilityMappingId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"PropertyFacilityMapping with id: {PropertyFacilityMappingId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned PropertyFacilityMapping with id: {PropertyFacilityMappingId}");
                    var PropertyFacilityMappingEntity = _mapper.Map<PropertyFacilityMappingSnapshot>(existingObj);
                    await _repo.DeletePropertyFacilityMapping(PropertyFacilityMappingEntity);
                    _logger.LogInfo($"Succesfully deleted PropertyFacilityMapping object with id {PropertyFacilityMappingEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted PropertyFacilityMapping with id {PropertyFacilityMappingEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePropertyFacilityMapping action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
