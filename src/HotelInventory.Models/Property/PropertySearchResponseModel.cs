using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Property
{
    public class PropertySearchResponseModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public int PropertyOwnerId { get; set; }
        public string PropertyContactPersonName { get; set; }
        public string PropertyContactEmail { get; set; }
        public string PropertyContactPhoneNo { get; set; }
        public string PropertyContactAlternatePhoneNo { get; set; }
        public int PropertyTypeId { get; set; }
        public int PropertySubTypeId { get; set; }
        public int Property_No_Of_Rooms { get; set; }
        public PropertySearchResponseAdrressModel PropertyAddress { get; set; }
        public List<PropertySearchResponseFacilityModel> PropertyFacilities { get; set; }
        public List<string> PropertyImageUrls { get; set; }
        public string PropertyHouseRules { get; set; }
        public string PropertyCancellationPolicy { get; set; }
        public List<PropertySearchResponseRoomModel> PropertyRooms { get; set; }
    }
    public class PropertySearchResponseAdrressModel
    {
        public long AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int Pincode { get; set; }
        public string PostOffice { get; set; }
        public string Landmark { get; set; }
        public List<PropertySearchResponseCountry_State_City_AreaModel> Areas { get; set; }
        public PropertySearchResponseGoogleMapDetailsModel GoogleMapDetails { get; set; }
    }
    public class PropertySearchResponseGoogleMapDetailsModel
    {
        public long GoogleMapDetailId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
    public class PropertySearchResponseCountry_State_City_AreaModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int AreaId { get; set; }
        public string Area { get; set; }
    }
    public class PropertySearchResponseFacilityModel
    {
        public int FacilityId { get; set; }
        public string Facility { get; set; }
    }
    public class PropertySearchResponseRoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public int RoomSubTypeId { get; set; }
        public string RoomSubType { get; set; }
        public string RoomSize { get; set; }
        public string Check_In_Time { get; set; }
        public string Check_Out_Time { get; set; }
        public bool IsSlotBookingEnabled { get; set; }
        public int Max_No_Of_Adults { get; set; }
        public int Max_No_Of_Child { get; set; }
        public bool IsFullDayRate { get; set; }
        public int Duration_hrs { get; set; }
        public int Tariff { get; set; }
        public List<PropertySearchResponseFacilityModel> RoomFacilities { get; set; }
        public List<string> RoomImageUrls { get; set; }
    }

}