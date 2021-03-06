using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.RoomFacilityMapping
{
    public class RoomFacilityMappingDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int FaciltiyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
