using HotelInventory.Models.Address;
using System.Collections.Generic;

namespace HotelInventory.Models.Property
{
    public class PropertyUploadRequestModel
    {
        public int PropertyTypeId { get; set; }
        public int PropertySubTypeId { get; set; }
        public int OwnerId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyRating { get; set; }
        public string PropertyContactName { get; set; }
        public string PropertyContactEmail { get; set; }
        public string PropertyContactPhoneNo { get; set; }
        public string PropertyContactAlternatePhoneNo { get; set; }
        public List<int> PropertyFaciltyIds { get; set; }
        public List<PropertyRoomUploadDetails> PropertyRooms { get; set; }
        public List<string> PropertyImagesUrls { get; set; }
        public string PropertyHouseRules { get; set; }
        public string PropertyCancellationPolicy { get; set; }
        public string PropertyGSTIn { get; set; }
        public List<int> PropertyPaymentTypes { get; set; }
        public double PropertyCommission { get; set; }
        public bool PropertyTnCAccepted { get; set; }
        public AddressDetails PropertyAddress { get; set; }
    }

    public class PropertyRoomUploadDetails
    {
        public int PropertyRoomTypeId { get; set; }
        public int PropertyRoomSubTypeId { get; set; }
        public List<int> PropertyRoomFaciltyIds { get; set; }
        public string PropertyRoomSize { get; set; }
        public int PropertyRoomTarrif { get; set; }
        public int Room_Max_No_Adults { get; set; }
        public int Room_Max_No_Childs { get; set; }
        public List<string> PropertyRoomImagesUrls { get; set; }
        public string RoomCheckInTime { get; set; }
        public string RoomCheckOutTime { get; set; }
    }
}
