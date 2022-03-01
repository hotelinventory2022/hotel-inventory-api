using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsSubscribedForNewsletter { get; set; }
        public bool IsSubscribedForPpromotion { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
