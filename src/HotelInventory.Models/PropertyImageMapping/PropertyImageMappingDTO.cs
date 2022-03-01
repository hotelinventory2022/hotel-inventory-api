using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.PropertyImageMapping
{
    public class PropertyImageMappingDTO
    {
        public long Id { get; set; }
        public int PropertyId { get; set; }
        public int RoomId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
