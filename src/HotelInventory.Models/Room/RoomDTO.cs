using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PropertyId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomSubTypeId { get; set; }
        public string RoomSize { get; set; }
        public string Check_In_Time { get; set; }
        public string Check_Out_Time { get; set; }
        public bool IsSlotBookingEnabled { get; set; }
        public int Max_No_Of_Adults { get; set; }
        public int Max_No_Of_Child { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
