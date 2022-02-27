using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.Roles;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class RoleService:IRoleService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRoleRepository _roleRepo;
        public RoleService(ILoggerManager logger, IMapper mapper, IRoleRepository roleRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _roleRepo = roleRepo;
        }
        public async Task<ApiResponse<IEnumerable<RoleDto>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepo.GetAllRolesAsync();
                _logger.LogInfo($"Returned all roles from database.");

                var roleResult = _mapper.Map<IEnumerable<RoleDto>>(roles);
                return new ApiResponse<IEnumerable<RoleDto>> { Data = roleResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all roles successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoles action: {ex.Message}");
                return new ApiResponse<IEnumerable<RoleDto>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoleDto>> GetRoleByIdAsync(int RoleId)
        {
            RoleDto roleResult = null;
            try
            {
                var role = await _roleRepo.GetRoleByIdAsync(RoleId);
                if (role == null)
                {
                    _logger.LogError($"Role with id: {RoleId}, hasn't been found in db.");
                    return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Role with id: { RoleId}, hasn't been found." };
                }
                else
                {
                    roleResult = _mapper.Map<RoleDto>(role);
                    _logger.LogInfo($"Returned role with id: {RoleId}");
                    return new ApiResponse<RoleDto> { Data = roleResult, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Successfully returned Role with id: { RoleId}" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRoleById action: {ex.Message}");
                return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoleDto>> GetRoleByNameAsync(string roleName)
        {
            RoleDto roleResult = null;
            try
            {
                if (string.IsNullOrEmpty(roleName))
                {
                    _logger.LogError($"No rolename has been passed by the client");
                    return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"No rolename has been passed by the client." };
                }
                else
                {
                    Expression<Func<RoleSnapshot, bool>> filter = _ => _.Name.Trim().ToUpper() == roleName.Trim().ToUpper();
                    var roles = await _roleRepo.GetFilteredRoleAsync(filter);
                    if (roles.Count() == 0)
                    {
                        _logger.LogInfo($"No role found with name: {roleName}");
                        return new ApiResponse<RoleDto> { Data = roleResult, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"No role found with name: {roleName}" };
                    }
                    else
                    {
                        roleResult = _mapper.Map<RoleDto>(roles.FirstOrDefault());
                        _logger.LogInfo($"Returned role with name: {roleName}");
                        return new ApiResponse<RoleDto> { Data = roleResult, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Returned role with name: {roleName}" };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRoleByName action: {ex.Message}");
                return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoleDto>> CreateRole(RoleDto Role)
        {
            RoleDto createdRole = null;
            try
            {
                if (Role == null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Role object sent from client is null" };
                }
                Expression<Func<RoleSnapshot, bool>> filter = _ => _.Name.Trim().ToUpper() == Role.Name.Trim().ToUpper();
                var role = await _roleRepo.GetFilteredRoleAsync(filter);
                if (role.Count() == 0)
                {
                    var roleEntity = _mapper.Map<RoleSnapshot>(Role);
                    await _roleRepo.CreateRole(roleEntity);
                    createdRole = _mapper.Map<RoleDto>(roleEntity);
                    _logger.LogError($"Succesfully created Role with id {roleEntity.Id.ToString()}.");
                    return new ApiResponse<RoleDto> { Data = createdRole, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {roleEntity.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"Role already exists with name - {role.FirstOrDefault().Name}.");
                    return new ApiResponse<RoleDto> { Data = createdRole, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Role already exists with name - {role.FirstOrDefault().Name}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRole action: {ex.Message}");
                return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<RoleDto>> UpdateRole(RoleDto Role)
        {
            RoleDto updatedObj = null;
            try
            {
                if (Role == null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Role object sent from client is null" };
                }

                var roleEntity = _mapper.Map<RoleSnapshot>(Role);

                await _roleRepo.UpdateRole(roleEntity);
                updatedObj = _mapper.Map<RoleDto>(roleEntity);

                _logger.LogError($"Succesfully updated Role object with id {roleEntity.Id.ToString()}.");
                return new ApiResponse<RoleDto> { Data = updatedObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated Role with id {roleEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRole action: {ex.Message}");
                return new ApiResponse<RoleDto> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeleteRole(int RoleId)
        {
            try
            {
                var role = await _roleRepo.GetRoleByIdAsync(RoleId);
                if (role == null)
                {
                    _logger.LogError($"Role with id: {RoleId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Role with id: { RoleId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned role with id: {RoleId}");
                    var roleEntity = _mapper.Map<RoleSnapshot>(role);
                    await _roleRepo.DeleteRole(role);
                    _logger.LogError($"Succesfully deleted Role object with id {roleEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted Role with id {roleEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRole action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
