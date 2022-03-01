using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.Address;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class AddressService : IAddressService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IAddressRepository _repo;
        public AddressService(ILoggerManager logger, IMapper mapper, IAddressRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<AddressDTO>>> GetAllAddresssAsync()
        {
            try
            {
                var addresses = await _repo.GetAllAddressesAsync();
                _logger.LogInfo($"Returned all addresses from database.");

                var addressResult = _mapper.Map<IEnumerable<AddressDTO>>(addresses);
                return new ApiResponse<IEnumerable<AddressDTO>> { Data = addressResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all addresses successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAddresssAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<AddressDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<AddressDTO>> CreateAddress(AddressDTO Address)
        {
            AddressDTO createdObj = null;
            try
            {
                if (Address == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return new ApiResponse<AddressDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Address object sent from client is null" };
                }
                Expression<Func<AddressSnapshot, bool>> filter = _ => _.AddressLine1.Trim().ToUpper() == Address.AddressLine1.Trim().ToUpper();
                var address = await _repo.GetFilteredAddressAsync(filter);
                if (address.Count() == 0)
                {
                    var addressEntity = _mapper.Map<AddressSnapshot>(Address);
                    await _repo.CreateAddress(addressEntity);
                    createdObj = _mapper.Map<AddressDTO>(addressEntity);
                    _logger.LogInfo($"Succesfully created Address with id {addressEntity.Id.ToString()}.");
                    return new ApiResponse<AddressDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {addressEntity.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"Address already exists with address line1 - {address.FirstOrDefault().AddressLine1}.");
                    return new ApiResponse<AddressDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Address already exists with address line1 - {address.FirstOrDefault().AddressLine1}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAddress action: {ex.Message}");
                return new ApiResponse<AddressDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<AddressDTO>> UpdateAddress(AddressDTO Address)
        {
            AddressDTO updatedobj = null;
            try
            {
                if (Address == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return new ApiResponse<AddressDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Address object sent from client is null" };
                }

                var addressEntity = _mapper.Map<AddressSnapshot>(Address);

                await _repo.UpdateAddress(addressEntity);
                updatedobj = _mapper.Map<AddressDTO>(addressEntity);

                _logger.LogInfo($"Succesfully updated Address object with id {addressEntity.Id.ToString()}.");
                return new ApiResponse<AddressDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated Address object with id {addressEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAddress action: {ex.Message}");
                return new ApiResponse<AddressDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeleteAddress(int AddressId)
        {
            try
            {
                Expression<Func<AddressSnapshot, bool>> filter = _ => _.Id == AddressId;
                var existingObj = await _repo.GetFilteredAddressAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"Address with id: {AddressId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Address with id: {AddressId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned address with id: {AddressId}");
                    var addressEntity = _mapper.Map<AddressSnapshot>(existingObj);
                    await _repo.DeleteAddress(addressEntity);
                    _logger.LogInfo($"Succesfully deleted address object with id {addressEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted address with id {addressEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAddress action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }

}
