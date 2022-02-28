using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("Users")]
    public class UserSnapshot
    {
        #region Constructor
        public UserSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Gender")]
        public string Gender { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("DOB")]
        public DateTime DOB { get; set; }
        [Column("IsEmailVerified")]
        public bool IsEmailVerified { get; set; }
        [Column("IsPhoneVerified")]
        public bool IsPhoneVerified { get; set; }
        [Column("IsSubscribedForNewsletter")]
        public bool IsSubscribedForNewsletter { get; set; }
        [Column("IsSubscribedForPpromotion")]
        public bool IsSubscribedForPpromotion { get; set; }
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
