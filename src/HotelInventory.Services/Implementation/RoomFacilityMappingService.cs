using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.RoomFacilityMapping;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class RoomFacilityMappingService : IRoomFacilityMappingService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRoomFacilityMappingRepository _repo;
        public RoomFacilityMappingService(ILoggerManager logger, IMapper mapper, IRoomFacilityMappingRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<RoomFacilityMappingDTO>>> GetAllRoomFacilityMappingsAsync()
        {
            try
            {
                var RoomFacilityMappinges = await _repo.GetAllRoomFacilityMappingesAsync();
                _logger.LogInfo($"Returned all RoomFacilityMappinges from database.");

                var RoomFacilityMappingResult = _mapper.Map<IEnumerable<RoomFacilityMappingDTO>>(RoomFacilityMappinges);
                return new ApiResponse<IEnumerable<RoomFacilityMappingDTO>> { Data = RoomFacilityMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all RoomFacilityMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoomFacilityMappingsAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<RoomFacilityMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<IEnumerable<RoomFacilityMappingDTO>>> GetFilteredRoomFacilityMappingsAsyncByRoomId(int RoomId)
        {
            try
            {
                Expression<Func<RoomFacilityMappingSnapshot, bool>> filter = _ => _.RoomId == RoomId;
                var RoomFacilityMappinges = await _repo.GetFilteredRoomFacilityMappingAsync(filter);
                _logger.LogInfo($"Returned filtered RoomFacilityMappinges from database.");

                var RoomFacilityMappingResult = _mapper.Map<IEnumerable<RoomFacilityMappingDTO>>(RoomFacilityMappinges);
                return new ApiResponse<IEnumerable<RoomFacilityMappingDTO>> { Data = RoomFacilityMappingResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned filtered RoomFacilityMappinges successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredRoomFacilityMappingsAsyncByRoomId action: {ex.Message}");
                return new ApiResponse<IEnumerable<RoomFacilityMappingDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoomFacilityMappingDTO>> CreateRoomFacilityMapping(RoomFacilityMappingDTO RoomFacilityMappingObj)
        {
            RoomFacilityMappingDTO createdObj = null;
            try
            {
                if (RoomFacilityMappingObj == null)
                {
                    _logger.LogError("RoomFacilityMapping object sent from client is null.");
                    return new ApiResponse<RoomFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "RoomFacilityMapping object sent from client is null" };
                }
                Expression<Func<RoomFacilityMappingSnapshot, bool>> filter = _ => _.RoomId == RoomFacilityMappingObj.RoomId && _.FaciltiyId == RoomFacilityMappingObj.FaciltiyId;
                var RoomFacilityMapping = await _repo.GetFilteredRoomFacilityMappingAsync(filter);
                if (RoomFacilityMapping.Count() == 0)
                {
                    var RoomFacilityMappingEntity = _mapper.Map<RoomFacilityMappingSnapshot>(RoomFacilityMappingObj);
                    await _repo.CreateRoomFacilityMapping(RoomFacilityMappingEntity);
                    createdObj = _mapper.Map<RoomFacilityMappingDTO>(RoomFacilityMappingEntity);
                    _logger.LogInfo($"Succesfully created RoomFacilityMapping with id {RoomFacilityMappingEntity.Id.ToString()}.");
                    return new ApiResponse<RoomFacilityMappingDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {RoomFacilityMappingEntity.Id.ToString()}." };
                }
                else
                {
                    var existingObj = _mapper.Map<RoomFacilityMappingDTO>(RoomFacilityMapping.FirstOrDefault());
                    _logger.LogError($"RoomFacilityMapping already exists with Facility - {RoomFacilityMapping.FirstOrDefault().FaciltiyId.ToString()}.");
                    return new ApiResponse<RoomFacilityMappingDTO> { Data = existingObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"RoomFacilityMapping already exists with Facility - {RoomFacilityMapping.FirstOrDefault().FaciltiyId.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRoomFacilityMapping action: {ex.Message}");
                return new ApiResponse<RoomFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        //public async Task<ApiResponse<RoomFacilityMappingDTO>> UpdateRoomFacilityMapping(RoomFacilityMappingDTO RoomFacilityMapping)
        //{
        //    RoomFacilityMappingDTO updatedobj = null;
        //    try
        //    {
        //        if (RoomFacilityMapping == null)
        //        {
        //            _logger.LogError("RoomFacilityMapping object sent from client is null.");
        //            return new ApiResponse<RoomFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "RoomFacilityMapping object sent from client is null" };
        //        }

        //        var RoomFacilityMappingEntity = _mapper.Map<RoomFacilityMappingSnapshot>(RoomFacilityMapping);

        //        await _repo.UpdateRoomFacilityMapping(RoomFacilityMappingEntity);
        //        updatedobj = _mapper.Map<RoomFacilityMappingDTO>(RoomFacilityMappingEntity);

        //        _logger.LogInfo($"Succesfully updated RoomFacilityMapping object with id {RoomFacilityMappingEntity.Id.ToString()}.");
        //        return new ApiResponse<RoomFacilityMappingDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated RoomFacilityMapping object with id {RoomFacilityMappingEntity.Id.ToString()}." };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside UpdateRoomFacilityMapping action: {ex.Message}");
        //        return new ApiResponse<RoomFacilityMappingDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
        //    }
        //}
        public async Task<ApiResponse<bool>> DeleteRoomFacilityMapping(int RoomFacilityMappingId)
        {
            try
            {
                Expression<Func<RoomFacilityMappingSnapshot, bool>> filter = _ => _.Id == RoomFacilityMappingId;
                var existingObj = await _repo.GetFilteredRoomFacilityMappingAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"RoomFacilityMapping with id: {RoomFacilityMappingId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"RoomFacilityMapping with id: {RoomFacilityMappingId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned RoomFacilityMapping with id: {RoomFacilityMappingId}");
                    var RoomFacilityMappingEntity = _mapper.Map<RoomFacilityMappingSnapshot>(existingObj);
                    await _repo.DeleteRoomFacilityMapping(RoomFacilityMappingEntity);
                    _logger.LogInfo($"Succesfully deleted RoomFacilityMapping object with id {RoomFacilityMappingEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted RoomFacilityMapping with id {RoomFacilityMappingEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRoomFacilityMapping action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
