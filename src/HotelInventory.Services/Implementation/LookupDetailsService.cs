using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.Lookup;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class LookupDetailsService : ILookupDetailsService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        public ILookupDetailsRepository _LookupDetailsRepo;
        public LookupDetailsService(ILoggerManager logger, IMapper mapper, ILookupDetailsRepository LookupDetailsRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _LookupDetailsRepo = LookupDetailsRepo;
        }
        public async Task<ApiResponse<IEnumerable<LookupDetailsDTO>>> GetAllLookupDetailssAsync()
        {
            try
            {
                var LookupDetailss = await _LookupDetailsRepo.GetAllLookupDetailssAsync();
                _logger.LogInfo($"Returned all LookupDetails from database.");

                var LookupDetailsResult = _mapper.Map<IEnumerable<LookupDetailsDTO>>(LookupDetailss);
                return new ApiResponse<IEnumerable<LookupDetailsDTO>> { Data = LookupDetailsResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all LookupDetails successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllLookupDetailss action: {ex.Message}");
                return new ApiResponse<IEnumerable<LookupDetailsDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<LookupDetailsDTO>> GetLookupDetailsByTypeAsync(string LookupType)
        {
            LookupDetailsDTO LookupDetailsResult = null;
            try
            {
                if (string.IsNullOrEmpty(LookupType))
                {
                    _logger.LogError($"No LookupType has been passed by the client");
                    return new ApiResponse<LookupDetailsDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"No LookupType has been passed by the client." };
                }
                else
                {
                    Expression<Func<LookupDetailsSnapshot, bool>> filter = _ => _.LookupType.Trim().ToUpper() == LookupType.Trim().ToUpper();
                    var LookupDetailss = await _LookupDetailsRepo.GetFilteredLookupDetailsAsync(filter);
                    if (LookupDetailss.Count() == 0)
                    {
                        _logger.LogInfo($"No LookupDetails found for type: {LookupType}");
                        return new ApiResponse<LookupDetailsDTO> { Data = LookupDetailsResult, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"No LookupDetails found for type: {LookupType}" };
                    }
                    else
                    {
                        LookupDetailsResult = _mapper.Map<LookupDetailsDTO>(LookupDetailss.FirstOrDefault());
                        _logger.LogInfo($"Returned LookupDetails for type: {LookupType}");
                        return new ApiResponse<LookupDetailsDTO> { Data = LookupDetailsResult, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Returned LookupDetails for type: {LookupType}" };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetLookupDetailsByName action: {ex.Message}");
                return new ApiResponse<LookupDetailsDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
