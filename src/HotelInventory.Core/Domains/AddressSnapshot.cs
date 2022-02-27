using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("Address")]
    public class AddressSnapshot
    {
        #region Constructor
        public AddressSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("GoogleMapId")]
        public long GoogleMapId { get; set; }
        [Column("AddressLine1")]
        public string AddressLine1 { get; set; }
        [Column("AddressLine2")]
        public string AddressLine2 { get; set; }
        [Column("Pincode")]
        public int Pincode { get; set; }
        [Column("PostOffice")]
        public string PostOffice { get; set; }
        [Column("Landmark")]
        public string Landmark { get; set; }
        [Column("Country_State_City_Area_Ids")]
        public string Country_State_City_Area_Ids { get; set; }
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
