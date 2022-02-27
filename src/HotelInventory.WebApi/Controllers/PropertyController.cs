using HotelInventory.Models;
using HotelInventory.Models.Property;
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
    public class PropertyController : Controller
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IPropertyService _propertyService;
        public PropertyController(ILogger<PropertyController> logger, IPropertyService propertyService)
        {
            _logger = logger;
            _propertyService = propertyService;
        }
        [HttpPost]
        [Route("UploadPropertyByOwner")]
        public async Task<ApiResponse<bool>> Create([FromBody] PropertyUploadRequestModel property)
        {
            var res = await _propertyService.UploadPropertyByOwner(property);
            return res;
        }
    }
}
