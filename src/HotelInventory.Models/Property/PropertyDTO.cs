using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Property
{
    public class PropertyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNo { get; set; }
        public string ContactAlternatePhoneNo { get; set; }
        public int PropertyTypeId { get; set; }
        public int PropertySubTypeId { get; set; }
        public int No_Of_Rooms { get; set; }
        public long AddressId { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string GSTInNo { get; set; }
        public bool IsTerms_ConditionAccepted { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
