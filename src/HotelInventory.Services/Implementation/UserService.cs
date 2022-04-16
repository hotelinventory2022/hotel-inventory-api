using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.User;
using HotelInventory.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class UserService : IUserService
    {
        private DateTime now = DateTime.Now;

        private ILoggerManager _logger;
        private IMapper _mapper;
        private IUserRepository _repo;
        private IUserRoleRepository _userRoleRepo;
        private readonly AppSettings _appSettings;
        public UserService(ILoggerManager logger, IMapper mapper, IUserRepository repo, IUserRoleRepository userRoleRepo, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
            _userRoleRepo = userRoleRepo;
            _appSettings = appSettings.Value;
        }
        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            try
            {
                var Useres = await _repo.GetAllUseresAsync();
                _logger.LogInfo($"Returned all Useres from database.");

                var UserResult = _mapper.Map<IEnumerable<UserDTO>>(Useres);
                return new ApiResponse<IEnumerable<UserDTO>> { Data = UserResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all Useres successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsersAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<UserDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<UserDTO>> CreateUser(UserDTO User)
        {
            UserDTO createdObj = null;
            try
            {
                if (User == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "User object sent from client is null" };
                }
                Expression<Func<UserSnapshot, bool>> filter = _ => _.Email.Trim().ToUpper() == User.Email.Trim().ToUpper();
                var user = await _repo.GetFilteredUserAsync(filter);
                if (user.Count() == 0)
                {
                    var UserEntity = _mapper.Map<UserSnapshot>(user);
                    await _repo.CreateUser(UserEntity);
                    createdObj = _mapper.Map<UserDTO>(UserEntity);
                    _logger.LogInfo($"Succesfully created User with id {UserEntity.Id.ToString()}.");
                    return new ApiResponse<UserDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created User with id {UserEntity.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"User already exists with email - {user.FirstOrDefault().Email}.");
                    return new ApiResponse<UserDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"User already exists with email - {user.FirstOrDefault().Email}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<UserDTO>> UpdateUser(UserDTO User)
        {
            UserDTO updatedobj = null;
            try
            {
                if (User == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "User object sent from client is null" };
                }

                var UserEntity = _mapper.Map<UserSnapshot>(User);

                await _repo.UpdateUser(UserEntity);
                updatedobj = _mapper.Map<UserDTO>(UserEntity);

                _logger.LogInfo($"Succesfully updated User object with id {UserEntity.Id.ToString()}.");
                return new ApiResponse<UserDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated User object with id {UserEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeleteUser(int UserId)
        {
            try
            {
                Expression<Func<UserSnapshot, bool>> filter = _ => _.Id == UserId;
                var existingObj = await _repo.GetFilteredUserAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"User with id: {UserId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"User with id: {UserId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned User with id: {UserId}");
                    var UserEntity = _mapper.Map<UserSnapshot>(existingObj);
                    await _repo.DeleteUser(UserEntity);
                    _logger.LogInfo($"Succesfully deleted User object with id {UserEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted User with id {UserEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<LoginResponse>> AuthenticateUser(LoginRequest request)
        {
            try
            {
                Expression<Func<UserSnapshot, bool>> filter = _ => _.Email.Trim().ToUpper() == request.Email.Trim().ToUpper() && _.Password.Trim().ToUpper() == request.Password.Trim().ToUpper();
                var existingObj = await _repo.GetFilteredUserAsync(filter);
                if (existingObj.Count() == 0)
                {
                    _logger.LogError($"User with email: {request.Email} hasn't been found in db.");
                    return new ApiResponse<LoginResponse> { Data = null, StatusCode = System.Net.HttpStatusCode.Unauthorized, Message = $"User with email: {request.Email} hasn't been found." };
                }
                else
                {
                    var existingUser = _mapper.Map<UserDTO>(existingObj.FirstOrDefault());
                    Expression<Func<UserRoleSnapshot, bool>> userRolefilter = _ => _.UserId == existingUser.Id && _.RoleId == request.RoleId;
                    var existingUserRoleObj = await _userRoleRepo.GetFilteredUserRolesAsync(userRolefilter);
                    var existingUserRole = _mapper.Map<UserRoleDTO>(existingUserRoleObj.FirstOrDefault());
                    // authentication successful so generate jwt and refresh tokens
                    var jwtToken = generateJwtToken(existingUser, existingUserRole);
                    _logger.LogInfo($"Authenticated User with id: {existingUser.Id.ToString()}");
                    return new ApiResponse<LoginResponse> 
                    { 
                        Data = new LoginResponse 
                        {
                            Id= existingUser.Id,
                            Name= existingUser.Name,
                            Email = existingUser.Email,
                            RoleId = existingUserRole.RoleId,
                            JwtToken = jwtToken
                        }, 
                        StatusCode = System.Net.HttpStatusCode.OK, 
                        Message = $"Succesfully authenticated User : {existingUser.Name}" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return new ApiResponse<LoginResponse> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<UserDTO>> RegisterUser(RegisterRequest request)
        {
            int loggedInUser = 1;
            UserDTO createdObj = null;
            try
            {
                Expression<Func<UserSnapshot, bool>> filter = _ => _.Email.Trim().ToUpper() == request.Email.Trim().ToUpper();
                var user = await _repo.GetFilteredUserAsync(filter);
                if (user.Count() == 0)
                {
                    //Check if password matches with confirm password
                    if(request.Password != request.ConfirmPassword)
                    {
                        _logger.LogError($"Password doesn't match.");
                        return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Password doesn't match." };
                    }
                    //Check if password conforms with password policy
                    if (request.Password.Length<8 && request.Password.Length>15)
                    {
                        _logger.LogError($"Password doesn't conform with password policy.");
                        return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Password doesn't conform with password policy." };
                    }
                    
                    var userRequest = new UserDTO
                    {
                        Name = request.Name,
                        Phone = request.Phone,
                        Email = request.Email,
                        Password = request.Password,
                        DOB = request.DOB,
                        Gender = request.Gender,
                        IsEmailVerified = request.IsEmailVerified,
                        IsPhoneVerified = request.IsPhoneVerified,
                        IsSubscribedForNewsletter = request.IsSubscribedForNewsletter,
                        IsSubscribedForPpromotion = request.IsSubscribedForPpromotion,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = loggedInUser,
                        CreatedOn = now,
                        LastUpdatedBy = loggedInUser,
                        LastUpdatedOn = now
                    };
                    var UserEntity = _mapper.Map<UserSnapshot>(userRequest);
                    await _repo.CreateUser(UserEntity);
                    createdObj = _mapper.Map<UserDTO>(UserEntity);
                    _logger.LogInfo($"Succesfully created User with id {createdObj.Id.ToString()}.");

                    var userRoleDTO = new UserRoleDTO
                    {
                        UserId = createdObj.Id,
                        RoleId = request.RoleId,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = loggedInUser,
                        CreatedOn = now,
                        LastUpdatedBy = loggedInUser,
                        LastUpdatedOn = now
                    };
                    var userRoleEntity = _mapper.Map<UserRoleSnapshot>(userRoleDTO);
                    await _userRoleRepo.CreateUserRole(userRoleEntity);
                    var createdUserRoleObj = _mapper.Map<UserRoleDTO>(userRoleEntity);
                    _logger.LogInfo($"Succesfully created User Role with id {createdUserRoleObj.Id.ToString()}.");
                    return new ApiResponse<UserDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created User with id {createdObj.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"User already exists with email - {user.FirstOrDefault().Email}.");
                    return new ApiResponse<UserDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"User already exists with email - {user.FirstOrDefault().Email}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside RegisterUser action: {ex.Message}");
                return new ApiResponse<UserDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        
        #region Helper methods
        private string generateJwtToken(UserDTO user, UserRoleDTO userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, userRole.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.JwTExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
