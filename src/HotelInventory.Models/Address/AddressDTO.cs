using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Address
{
    public class AddressDTO
    {
        public long Id { get; set; }
        public long GoogleMapId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int Pincode { get; set; }
        public string PostOffice { get; set; }
        public string Landmark { get; set; }
        public string Country_State_City_Area_Ids { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
