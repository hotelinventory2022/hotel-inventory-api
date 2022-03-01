using HotelInventory.Models;
using HotelInventory.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsersAsync();
        Task<ApiResponse<UserDTO>> CreateUser(UserDTO User);
        Task<ApiResponse<UserDTO>> UpdateUser(UserDTO User);
        Task<ApiResponse<bool>> DeleteUser(int UserId);
        Task<ApiResponse<LoginResponse>> AuthenticateUser(LoginRequest request);
        Task<ApiResponse<UserDTO>> RegisterUser(RegisterRequest request);
    }
}
