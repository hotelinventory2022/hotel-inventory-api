using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.GoogleMapDetails;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class GoogleMapDetailsService : IGoogleMapDetailsService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IGoogleMapDetailsRepository _repo;
        public GoogleMapDetailsService(ILoggerManager logger, IMapper mapper, IGoogleMapDetailsRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ApiResponse<IEnumerable<GoogleMapDetailsDto>>> GetAllGoogleMapDetailssAsync()
        {
            try
            {
                var GoogleMapDetailses = await _repo.GetAllGoogleMapDetailsAsync();
                _logger.LogInfo($"Returned all GoogleMapDetailses from database.");

                var GoogleMapDetailsResult = _mapper.Map<IEnumerable<GoogleMapDetailsDto>>(GoogleMapDetailses);
                return new ApiResponse<IEnumerable<GoogleMapDetailsDto>> { Data = GoogleMapDetailsResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all GoogleMapDetailses successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllGoogleMapDetailssAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<GoogleMapDetailsDto>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<GoogleMapDetailsDto>> CreateGoogleMapDetails(GoogleMapDetailsDto GoogleMapDetails)
        {
            GoogleMapDetailsDto createdObj = null;
            try
            {
                if (GoogleMapDetails == null)
                {
                    _logger.LogError("GoogleMapDetails object sent from client is null.");
                    return new ApiResponse<GoogleMapDetailsDto> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "GoogleMapDetails object sent from client is null" };
                }
                Expression<Func<GoogleMapDetailsSnapshot, bool>> filter = _ => _.Latitude == GoogleMapDetails.Latitude && _.Longitude == GoogleMapDetails.Longitude;
                var googleMapDetails = await _repo.GetFilteredGoogleMapDetailsAsync(filter);
                if (googleMapDetails.Count() == 0)
                {
                    var GoogleMapDetailsEntity = _mapper.Map<GoogleMapDetailsSnapshot>(GoogleMapDetails);
                    await _repo.CreateGoogleMapDetails(GoogleMapDetailsEntity);
                    createdObj = _mapper.Map<GoogleMapDetailsDto>(GoogleMapDetailsEntity);
                    _logger.LogInfo($"Succesfully created GoogleMapDetails with id {GoogleMapDetailsEntity.Id.ToString()}.");
                    return new ApiResponse<GoogleMapDetailsDto> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {GoogleMapDetailsEntity.Id.ToString()}." };
                }
                else
                {
                    var existingObj = _mapper.Map<GoogleMapDetailsDto>(googleMapDetails.FirstOrDefault());
                    _logger.LogError($"GoogleMapDetails already exists with latitude - {googleMapDetails.FirstOrDefault().Latitude.ToString()} and longitude - {googleMapDetails.FirstOrDefault().Longitude.ToString()}.");
                    return new ApiResponse<GoogleMapDetailsDto> { Data = existingObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"GoogleMapDetails already exists with latitude - {googleMapDetails.FirstOrDefault().Latitude.ToString()} and longitude - {googleMapDetails.FirstOrDefault().Longitude.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateGoogleMapDetails action: {ex.Message}");
                return new ApiResponse<GoogleMapDetailsDto> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<GoogleMapDetailsDto>> UpdateGoogleMapDetails(GoogleMapDetailsDto GoogleMapDetails)
        {
            GoogleMapDetailsDto updatedobj = null;
            try
            {
                if (GoogleMapDetails == null)
                {
                    _logger.LogError("GoogleMapDetails object sent from client is null.");
                    return new ApiResponse<GoogleMapDetailsDto> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "GoogleMapDetails object sent from client is null" };
                }

                var GoogleMapDetailsEntity = _mapper.Map<GoogleMapDetailsSnapshot>(GoogleMapDetails);

                await _repo.UpdateGoogleMapDetails(GoogleMapDetailsEntity);
                updatedobj = _mapper.Map<GoogleMapDetailsDto>(GoogleMapDetailsEntity);

                _logger.LogInfo($"Succesfully updated GoogleMapDetails object with id {GoogleMapDetailsEntity.Id.ToString()}.");
                return new ApiResponse<GoogleMapDetailsDto> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated GoogleMapDetails object with id {GoogleMapDetailsEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateGoogleMapDetails action: {ex.Message}");
                return new ApiResponse<GoogleMapDetailsDto> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeleteGoogleMapDetails(int GoogleMapDetailsId)
        {
            try
            {
                Expression<Func<GoogleMapDetailsSnapshot, bool>> filter = _ => _.Id == GoogleMapDetailsId;
                var existingObj = await _repo.GetFilteredGoogleMapDetailsAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"GoogleMapDetails with id: {GoogleMapDetailsId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"GoogleMapDetails with id: {GoogleMapDetailsId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned GoogleMapDetails with id: {GoogleMapDetailsId}");
                    var GoogleMapDetailsEntity = _mapper.Map<GoogleMapDetailsSnapshot>(existingObj);
                    await _repo.DeleteGoogleMapDetails(GoogleMapDetailsEntity);
                    _logger.LogInfo($"Succesfully deleted GoogleMapDetails object with id {GoogleMapDetailsEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted GoogleMapDetails with id {GoogleMapDetailsEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteGoogleMapDetails action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
