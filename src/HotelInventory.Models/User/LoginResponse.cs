using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HotelInventory.Models.User
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

        //[JsonIgnore] // refresh token is returned in http only cookie
        //public string RefreshToken { get; set; }
    }
}
