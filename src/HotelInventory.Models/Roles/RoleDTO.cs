using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Roles
{
    public class RoleDto
    {
        #region Property   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        #endregion Property
    }
}
