using System.Collections.Generic;

namespace HotelInventory.Models.Address
{
    public class AddressDetails
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        #region owner upload only
        public List<int> AreaIds { get; set; }
        public string Landmark { get; set; }
        #endregion
    }
}
