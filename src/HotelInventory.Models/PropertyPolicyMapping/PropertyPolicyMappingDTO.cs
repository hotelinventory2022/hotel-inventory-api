using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.PropertyPolicyMapping
{
    public class PropertyPolicyMappingDTO
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string HouseRules { get; set; }
        public string CancellationPolicy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
