using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.User
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
