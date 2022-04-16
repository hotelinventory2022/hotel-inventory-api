using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.User
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DOB { get; set; }
        public int RoleId { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsSubscribedForNewsletter { get; set; }
        public bool IsSubscribedForPpromotion { get; set; }
    }
}
