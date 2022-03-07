using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.Room;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class RoomService : IRoomService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRoomRepository _repo;
        public RoomService(ILoggerManager logger, IMapper mapper, IRoomRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<RoomDTO>>> GetAllRoomsAsync()
        {
            try
            {
                var Roomes = await _repo.GetAllRoomesAsync();
                _logger.LogInfo($"Returned all Roomes from database.");

                var RoomResult = _mapper.Map<IEnumerable<RoomDTO>>(Roomes);
                return new ApiResponse<IEnumerable<RoomDTO>> { Data = RoomResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all Roomes successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoomsAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<RoomDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<IEnumerable<RoomDTO>>> GetFilteredRoomsAsyncByRoomId(int RoomId)
        {
            try
            {
                Expression<Func<RoomSnapshot, bool>> filter = _ => _.Id == RoomId;
                var Roomes = await _repo.GetFilteredRoomAsync(filter);
                _logger.LogInfo($"Returned filtered Roomes from database.");

                var RoomResult = _mapper.Map<IEnumerable<RoomDTO>>(Roomes);
                return new ApiResponse<IEnumerable<RoomDTO>> { Data = RoomResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned filtered Roomes successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredRoomsAsyncByRoomId action: {ex.Message}");
                return new ApiResponse<IEnumerable<RoomDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoomDTO>> CreateRoom(RoomDTO RoomObj)
        {
            RoomDTO createdObj = null;
            try
            {
                if (RoomObj == null)
                {
                    _logger.LogError("Room object sent from client is null.");
                    return new ApiResponse<RoomDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Room object sent from client is null" };
                }
                var RoomEntity = _mapper.Map<RoomSnapshot>(RoomObj);
                await _repo.CreateRoom(RoomEntity);
                createdObj = _mapper.Map<RoomDTO>(RoomEntity);
                _logger.LogInfo($"Succesfully created Room with id {RoomEntity.Id.ToString()}.");
                return new ApiResponse<RoomDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {RoomEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRoom action: {ex.Message}");
                return new ApiResponse<RoomDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoomDTO>> UpdateRoom(RoomDTO Room)
        {
            RoomDTO updatedobj = null;
            try
            {
                if (Room == null)
                {
                    _logger.LogError("Room object sent from client is null.");
                    return new ApiResponse<RoomDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Room object sent from client is null" };
                }

                var RoomEntity = _mapper.Map<RoomSnapshot>(Room);

                await _repo.UpdateRoom(RoomEntity);
                updatedobj = _mapper.Map<RoomDTO>(RoomEntity);

                _logger.LogInfo($"Succesfully updated Room object with id {RoomEntity.Id.ToString()}.");
                return new ApiResponse<RoomDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated Room object with id {RoomEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRoom action: {ex.Message}");
                return new ApiResponse<RoomDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeleteRoom(int RoomId)
        {
            try
            {
                Expression<Func<RoomSnapshot, bool>> filter = _ => _.Id == RoomId;
                var existingObj = await _repo.GetFilteredRoomAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"Room with id: {RoomId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Room with id: {RoomId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned Room with id: {RoomId}");
                    var RoomEntity = _mapper.Map<RoomSnapshot>(existingObj);
                    await _repo.DeleteRoom(RoomEntity);
                    _logger.LogInfo($"Succesfully deleted Room object with id {RoomEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted Room with id {RoomEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRoom action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
