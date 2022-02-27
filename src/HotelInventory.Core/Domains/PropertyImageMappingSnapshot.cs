using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("PropertyImageMapping")]
    public class PropertyImageMappingSnapshot
    {
        #region Constructor
        public PropertyImageMappingSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("PropertyId")]
        public int PropertyId { get; set; }
        [Column("RoomId")]
        public int RoomId { get; set; }
        [Column("ImageUrl")]
        public string ImageUrl { get; set; }
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
