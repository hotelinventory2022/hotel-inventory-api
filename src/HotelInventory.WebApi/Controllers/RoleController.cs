using HotelInventory.Models;
using HotelInventory.Models.Roles;
using HotelInventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelInventory.WebApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;
        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }        
        [HttpGet]
        [Route("GetRoles")]
        public async Task<ApiResponse<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return roles;
        }        
        [HttpGet]
        [Route("GetRoleById/{roleId:int}")]
        public async Task<ApiResponse<RoleDto>> GetById(int roleId)
        {
            var roles = await _roleService.GetRoleByIdAsync(roleId);
            return roles;
        }
        [HttpGet]
        [Route("GetRoleByName/{roleName}")]
        public async Task<ApiResponse<RoleDto>> GetByName(string roleName)
        {
            
            var roles = await _roleService.GetRoleByNameAsync(roleName);
            return roles;
        }
        [HttpPost]
        [Route("CreateRole")]
        public async Task<ApiResponse<RoleDto>> Create([FromBody]RoleDto role)
        {
            var roles = await _roleService.CreateRole(role);
            return roles;
        }
        [HttpPut]
        [Route("UpdateRole")]
        public async Task<ApiResponse<RoleDto>> Update([FromBody] RoleDto role)
        {
            var roles = await _roleService.UpdateRole(role);
            return roles;
        }
        [HttpDelete]
        [Route("DeleteRole/{roleId:int}")]
        public async Task<ApiResponse<bool>> Delete(int roleId)
        {
            var roles = await _roleService.DeleteRole(roleId);
            return roles;
        }
    }
}
