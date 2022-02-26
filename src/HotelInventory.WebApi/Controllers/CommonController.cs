using HotelInventory.Models;
using HotelInventory.Models.Lookup;
using HotelInventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelInventory.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CommonController : Controller
    {
        private readonly ILogger<CommonController> _logger;
        private readonly ILookupDetailsService _lookupDetailsService;
        public CommonController(ILogger<CommonController> logger, ILookupDetailsService lookupDetailsService)
        {
            _logger = logger;
            _lookupDetailsService = lookupDetailsService;
        }
        [HttpGet]
        [Route("GetLookupDetails")]
        public async Task<ApiResponse<IEnumerable<LookupDetailsDTO>>> Get()
        {
            var lookups = await _lookupDetailsService.GetAllLookupDetailssAsync();
            return lookups;
        }
        [HttpGet]
        [Route("GetLookupDetailsByType/{lookupType}")]
        public async Task<ApiResponse<LookupDetailsDTO>> GetByName(string lookupType)
        {

            var roles = await _lookupDetailsService.GetLookupDetailsByTypeAsync(lookupType);
            return roles;
        }
    }
}
