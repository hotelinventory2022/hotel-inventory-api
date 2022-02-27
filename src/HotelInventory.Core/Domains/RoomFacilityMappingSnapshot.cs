using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("RoomFacilityMapping")]
    public class RoomFacilityMappingSnapshot
    {
        #region Constructor
        public RoomFacilityMappingSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("RoomId")]
        public int RoomId { get; set; }
        [Column("FaciltiyId")]
        public int FaciltiyId { get; set; }
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
