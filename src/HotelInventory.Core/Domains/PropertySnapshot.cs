using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("Property")]
    public class PropertySnapshot
    {
        #region Constructor
        public PropertySnapshot()
        {

        }
        #endregion Constructor

        #region Property 
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("OwnerId")]
        public int OwnerId { get; set; }
        [Column("ContactPersonName")]
        public string ContactPersonName { get; set; }
        [Column("ContactEmail")]
        public string ContactEmail { get; set; }
        [Column("ContactPhoneNo")]
        public string ContactPhoneNo { get; set; }
        [Column("ContactAlternatePhoneNo")]
        public string ContactAlternatePhoneNo { get; set; }
        [Column("PropertyTypeId")]
        public int PropertyTypeId { get; set; }
        [Column("PropertySubTypeId")]
        public int PropertySubTypeId { get; set; }
        [Column("No_Of_Rooms")]
        public int No_Of_Rooms { get; set; }
        [Column("AddressId")]
        public long AddressId { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Rating")]
        public string Rating { get; set; }
        [Column("GSTInNo")]
        public string GSTInNo { get; set; }
        [Column("IsTerms_ConditionAccepted")]
        public bool IsTerms_ConditionAccepted { get; set; }
        [Column("PublishedOn")]
        public DateTime PublishedOn { get; set; }
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

        #endregion Property
    }
}
