using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelInventory.Services.Interfaces;
using HotelInventory.Models.Roles;
using HotelInventory.Models;
using HotelInventory.Models.Room;

namespace HotelInventory.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IAvailabilityService _availabilityService;
        public RoomController(ILogger<RoomController> logger, IAvailabilityService availabilityService)
        {
            _logger = logger;
            _availabilityService = availabilityService;
        }
        [HttpPost]
        [Route("GetAvailabilty")]
        public async Task<ApiResponse<IEnumerable<AvailabiltyDTO>>> Get([FromBody] AvailabiltyRequestModel availabilty)
        {
            try
            {
                availabilty = verifyRequest(availabilty,false);
                var roomAvailabilty = await _availabilityService.GetAvailibiltyAsync(availabilty);
                return roomAvailabilty;
            }
            catch (NotSupportedException ex)
            {
                return errorResponse_get(ex.Message);
            }
            catch(Exception ex)
            {
                return errorResponse_get(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateAvailabilty")]
        public async Task<ApiResponse<AvailabiltyDTO>> Update([FromBody] AvailabiltyRequestModel availabiltyRequest)
        {
            try
            {
                availabiltyRequest = verifyRequest(availabiltyRequest,true);
                var roomAvailabilty = await _availabilityService.CreateOrUpdateAvailibilty(availabiltyRequest);
                return roomAvailabilty;
            }
            catch (NotSupportedException ex)
            {
                return errorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return errorResponse(ex.Message);
            }
        }

        #region private method
        private ApiResponse<AvailabiltyDTO> errorResponse(string message)
        {
            return new ApiResponse<AvailabiltyDTO>
            {
                Data = null,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = message
            };
        }
        private ApiResponse<IEnumerable<AvailabiltyDTO>> errorResponse_get(string message)
        {
            return new ApiResponse<IEnumerable<AvailabiltyDTO>>
            {
                Data = null,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = message
            };
        }
        private AvailabiltyRequestModel verifyRequest(AvailabiltyRequestModel availabilty, bool checkRoom)
        {
            if (availabilty.StartDate == DateTime.MinValue || availabilty.StartDate.DayOfYear < DateTime.Now.DayOfYear || availabilty.EndDate == DateTime.MinValue || availabilty.EndDate.DayOfYear < DateTime.Now.DayOfYear)
            {
                throw new NotSupportedException("StartDate, EndDate should be greater than todays date");
            }
            if (checkRoom)
                availabilty.ListOfAvailabilties.ForEach(av =>
                {
                    if (av.No_Of_Guests < 0 || av.Tariff < 0 || av.Duration_hrs < 0)
                    {
                        throw new NotSupportedException("RoomId, No_Of_Guests, Tariff, Duration_hrs should be greater than 0");
                    }
                    else if (av.StartDate == DateTime.MinValue || av.StartDate < DateTime.Now || av.EndDate == DateTime.MinValue || av.EndDate < DateTime.Now)
                    {
                        throw new NotSupportedException("StartDate, EndDate should be greater than todays date");
                    }
                });
            return availabilty;
        }
        #endregion
    }
}
