using HotelInventory.Models;
using HotelInventory.Models.User;
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
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("authenticate")]
        public async Task<ApiResponse<LoginResponse>> Authenticate([FromBody] LoginRequest model)
        {
            var response = await _userService.AuthenticateUser(model);

            return response;
        }
        [AllowAnonymous]
        [HttpPost()]
        [Route("register")]
        public async Task<ApiResponse<UserDTO>> Register([FromBody] RegisterRequest model)
        {
            var response = await _userService.RegisterUser(model);

            return response;
        }
    }
}
