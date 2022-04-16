using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("UserRoleMapping")]
    public class UserRoleSnapshot
    {
        #region Constructor
        public UserRoleSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("RoleId")]
        public int RoleId { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
        [Column("CreatedBy")]
        public int CreatedBy { get; set; }
        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [Column("LastUpdatedBy")]
        public int LastUpdatedBy { get; set; }
        [Column("LastUpdatedOn")]
        public DateTime LastUpdatedOn { get; set; }
        #endregion
    }
}
