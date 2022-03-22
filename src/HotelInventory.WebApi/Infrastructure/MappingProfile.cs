using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Models.Address;
using HotelInventory.Models.GoogleMapDetails;
using HotelInventory.Models.Lookup;
using HotelInventory.Models.Property;
using HotelInventory.Models.PropertyFacilityMapping;
using HotelInventory.Models.PropertyImageMapping;
using HotelInventory.Models.PropertyPolicyMapping;
using HotelInventory.Models.Roles;
using HotelInventory.Models.Room;
using HotelInventory.Models.RoomFacilityMapping;
using HotelInventory.Models.User;

namespace HotelInventory.WebApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AddressSnapshot, AddressDTO>();

            CreateMap<AddressDTO, AddressSnapshot>();

            CreateMap<GoogleMapDetailsSnapshot, GoogleMapDetailsDto>();

            CreateMap<GoogleMapDetailsDto, GoogleMapDetailsSnapshot>();

            CreateMap<LookupDetailsSnapshot, LookupDetailsDTO>();

            CreateMap<LookupDetailsDTO, LookupDetailsSnapshot>();

            CreateMap<PropertyFacilityMappingSnapshot, PropertyFacilityMappingDTO>();

            CreateMap<PropertyFacilityMappingDTO, PropertyFacilityMappingSnapshot>();

            CreateMap<PropertyImageMappingSnapshot, PropertyImageMappingDTO>();

            CreateMap<PropertyImageMappingDTO, PropertyImageMappingSnapshot>();

            CreateMap<PropertyPolicyMappingSnapshot, PropertyPolicyMappingDTO>();

            CreateMap<PropertyPolicyMappingDTO, PropertyPolicyMappingSnapshot>();

            CreateMap<PropertySnapshot, PropertyDTO>();

            CreateMap<PropertyDTO, PropertySnapshot>();

            CreateMap<RoleSnapshot, RoleDto>();

            CreateMap<RoleDto, RoleSnapshot>();

            CreateMap<RoomFacilityMappingSnapshot, RoomFacilityMappingDTO>();

            CreateMap<RoomFacilityMappingDTO, RoomFacilityMappingSnapshot>();

            CreateMap<RoomSnapshot, RoomDTO>();

            CreateMap<RoomDTO, RoomSnapshot>();

            CreateMap<UserSnapshot, UserDTO>();

            CreateMap<UserDTO, UserSnapshot>();

            CreateMap<AvailabiltyRateSnapshot, AvailabiltyDTO>();

            CreateMap<AvailabiltyDTO, AvailabiltyRateSnapshot>();

        }
    }
}
