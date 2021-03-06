using HotelInventory.Models;
using HotelInventory.Models.Property;
using HotelInventory.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize]
        [HttpPost]
        [Route("SearchProperty")]
        public async Task<ApiResponse<PaginatedResponseModel<List<PropertySearchResponseModel>>>> SearchProperty([FromBody] PaginatedRequestModel<PropertySearchRequestModel> searchModel)
        {
            var res = await _propertyService.SearchProperty(searchModel);
            return res;
        }
        [Authorize]
        [HttpPost]
        [Route("UploadPropertyByOwner")]
        public async Task<ApiResponse<bool>> UploadPropertyByOwner([FromBody] PropertyUploadRequestModel property)
        {
            var res = await _propertyService.UploadPropertyByOwner(property);
            return res;
        }
    }
}
