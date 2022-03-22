using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("Country_State_City_Area")]
    public class Country_State_City_AreaSnapshot
    {
        #region Constructor
        public Country_State_City_AreaSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("CountryId")]
        public int CountryId { get; set; }
        [Column("CountryName")]
        public string CountryName { get; set; }
        [Column("StateId")]
        public int StateId { get; set; }
        [Column("StateName")]
        public string StateName { get; set; }
        [Column("CityId")]
        public int CityId { get; set; }
        [Column("CityName")]
        public string CityName { get; set; }
        [Column("AreaId")]
        public int AreaId { get; set; }
        [Column("AreaName")]
        public string AreaName { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
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
