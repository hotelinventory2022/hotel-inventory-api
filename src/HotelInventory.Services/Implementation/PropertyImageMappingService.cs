using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.PropertyImageMapping;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class PropertyImageMappingService : IPropertyImageMappingService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IPropertyImageMappingRepository _repo;
        public PropertyImageMappingService(ILoggerManager logger, IMapper mapper, IPropertyImageMappingRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<PropertyImageMappingDTO>>> GetAllPropertyImageMappingsAsync()
        {
            try
            {
                var PropertyImageMappinges = await _repo.GetAllPropertyImageMappingesAsync();
                _logger.LogInfo($"Returned all PropertyImageMappinges from database.");

                var PropertyImageMappingResult = _mapper.Map<IEnumerable<PropertyImageMappingDTO>>(PropertyImageMappinges);
                return new ApiResponse<IEnumerable<PropertyImageMappingDTO>> { Data = PropertyImageMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all PropertyImageMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPropertyImageMappingsAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyImageMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<IEnumerable<PropertyImageMappingDTO>>> GetFilteredPropertyImageMappingsAsyncByPropertyId(int PropertyId)
        {
            try
            {
                Expression<Func<PropertyImageMappingSnapshot, bool>> filter = _ => _.PropertyId == PropertyId;
                var PropertyImageMappinges = await _repo.GetFilteredPropertyImageMappingAsync(filter);
                _logger.LogInfo($"Returned filtered PropertyImageMappinges from database.");

                var PropertyImageMappingResult = _mapper.Map<IEnumerable<PropertyImageMappingDTO>>(PropertyImageMappinges);
                return new ApiResponse<IEnumerable<PropertyImageMappingDTO>> { Data = PropertyImageMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned filtered PropertyImageMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredPropertyImageMappingsAsyncByPropertyId action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyImageMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<PropertyImageMappingDTO>> CreatePropertyImageMapping(PropertyImageMappingDTO propertyImageMappingObj)
        {
            PropertyImageMappingDTO createdObj = null;
            try
            {
                if (propertyImageMappingObj == null)
                {
                    _logger.LogError("PropertyImageMapping object sent from client is null.");
                    return new ApiResponse<PropertyImageMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "PropertyImageMapping object sent from client is null" };
                }
                Expression<Func<PropertyImageMappingSnapshot, bool>> filter = _ => _.PropertyId == propertyImageMappingObj.PropertyId && _.ImageUrl == propertyImageMappingObj.ImageUrl;
                var PropertyImageMapping = await _repo.GetFilteredPropertyImageMappingAsync(filter);
                if (PropertyImageMapping.Count() == 0)
                {
                    var PropertyImageMappingEntity = _mapper.Map<PropertyImageMappingSnapshot>(PropertyImageMapping);
                    await _repo.CreatePropertyImageMapping(PropertyImageMappingEntity);
                    createdObj = _mapper.Map<PropertyImageMappingDTO>(PropertyImageMappingEntity);
                    _logger.LogInfo($"Succesfully created PropertyImageMapping with id {PropertyImageMappingEntity.Id.ToString()}.");
                    return new ApiResponse<PropertyImageMappingDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {PropertyImageMappingEntity.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"PropertyImageMapping already exists with Image - {PropertyImageMapping.FirstOrDefault().ImageUrl.ToString()}.");
                    return new ApiResponse<PropertyImageMappingDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"PropertyImageMapping already exists with Image - {PropertyImageMapping.FirstOrDefault().ImageUrl.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePropertyImageMapping action: {ex.Message}");
                return new ApiResponse<PropertyImageMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        //public async Task<ApiResponse<PropertyImageMappingDTO>> UpdatePropertyImageMapping(PropertyImageMappingDTO PropertyImageMapping)
        //{
        //    PropertyImageMappingDTO updatedobj = null;
        //    try
        //    {
        //        if (PropertyImageMapping == null)
        //        {
        //            _logger.LogError("PropertyImageMapping object sent from client is null.");
        //            return new ApiResponse<PropertyImageMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "PropertyImageMapping object sent from client is null" };
        //        }

        //        var PropertyImageMappingEntity = _mapper.Map<PropertyImageMappingSnapshot>(PropertyImageMapping);

        //        await _repo.UpdatePropertyImageMapping(PropertyImageMappingEntity);
        //        updatedobj = _mapper.Map<PropertyImageMappingDTO>(PropertyImageMappingEntity);

        //        _logger.LogInfo($"Succesfully updated PropertyImageMapping object with id {PropertyImageMappingEntity.Id.ToString()}.");
        //        return new ApiResponse<PropertyImageMappingDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated PropertyImageMapping object with id {PropertyImageMappingEntity.Id.ToString()}." };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside UpdatePropertyImageMapping action: {ex.Message}");
        //        return new ApiResponse<PropertyImageMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
        //    }
        //}
        public async Task<ApiResponse<bool>> DeletePropertyImageMapping(int PropertyImageMappingId)
        {
            try
            {
                Expression<Func<PropertyImageMappingSnapshot, bool>> filter = _ => _.Id == PropertyImageMappingId;
                var existingObj = await _repo.GetFilteredPropertyImageMappingAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"PropertyImageMapping with id: {PropertyImageMappingId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"PropertyImageMapping with id: {PropertyImageMappingId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned PropertyImageMapping with id: {PropertyImageMappingId}");
                    var PropertyImageMappingEntity = _mapper.Map<PropertyImageMappingSnapshot>(existingObj);
                    await _repo.DeletePropertyImageMapping(PropertyImageMappingEntity);
                    _logger.LogInfo($"Succesfully deleted PropertyImageMapping object with id {PropertyImageMappingEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted PropertyImageMapping with id {PropertyImageMappingEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePropertyImageMapping action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
