using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("Rooms")]
    public class RoomSnapshot
    {
        #region Constructor
        public RoomSnapshot()
        {

        }
        #endregion Constructor
        #region Property   
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("PropertyId")]
        public int PropertyId { get; set; }
        [Column("RoomTypeId")]
        public int RoomTypeId { get; set; }
        [Column("RoomSubTypeId")]
        public int RoomSubTypeId { get; set; }
        [Column("RoomSize")]
        public string RoomSize { get; set; }
        [Column("Check_In_Time")]
        public string Check_In_Time { get; set; }
        [Column("Check_Out_Time")]
        public string Check_Out_Time { get; set; }
        [Column("IsSlotBookingEnabled")]
        public bool IsSlotBookingEnabled { get; set; }
        [Column("Max_No_Of_Adults")]
        public int Max_No_Of_Adults { get; set; }
        [Column("Max_No_Of_Child")]
        public int Max_No_Of_Child { get; set; }
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
