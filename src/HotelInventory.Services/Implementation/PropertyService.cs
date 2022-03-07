using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.Address;
using HotelInventory.Models.GoogleMapDetails;
using HotelInventory.Models.Property;
using HotelInventory.Models.PropertyFacilityMapping;
using HotelInventory.Models.PropertyImageMapping;
using HotelInventory.Models.PropertyPolicyMapping;
using HotelInventory.Models.Room;
using HotelInventory.Models.RoomFacilityMapping;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class PropertyService : IPropertyService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IPropertyRepository _repo;
        private IGoogleMapDetailsService _gmapService;
        private IAddressService _addressService;
        private IPropertyFacilityMappingService _propertyFacilityMappingService;
        private IPropertyPolicyMappingService _propertyPolicyMappingService;
        private IPropertyImageMappingService _propertImageMappingService;
        private IRoomService _roomService;
        private IRoomFacilityMappingService _roomFacilityMappingService;
        public PropertyService(ILoggerManager logger, IMapper mapper, IPropertyRepository repo, IGoogleMapDetailsService gmapService, IAddressService addressService,
            IPropertyFacilityMappingService propertyFacilityMappingService, IPropertyPolicyMappingService propertyPolicyMappingService, IPropertyImageMappingService propertImageMappingService, 
            IRoomService roomService, IRoomFacilityMappingService roomFacilityMappingService)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
            _gmapService = gmapService;
            _addressService = addressService;
            _propertyFacilityMappingService = propertyFacilityMappingService;
            _propertyPolicyMappingService = propertyPolicyMappingService;
            _propertImageMappingService = propertImageMappingService;
            _roomService = roomService;
            _roomFacilityMappingService = roomFacilityMappingService;
        }
        public async Task<ApiResponse<IEnumerable<PropertyDTO>>> GetAllPropertysAsync()
        {
            try
            {
                var Propertyes = await _repo.GetAllPropertyesAsync();
                _logger.LogInfo($"Returned all Propertyes from database.");

                var PropertyResult = _mapper.Map<IEnumerable<PropertyDTO>>(Propertyes);
                return new ApiResponse<IEnumerable<PropertyDTO>> { Data = PropertyResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all Propertyes successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPropertysAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<IEnumerable<PropertyDTO>>> GetFilteredPropertysAsyncByPropertyId(int PropertyId)
        {
            try
            {
                Expression<Func<PropertySnapshot, bool>> filter = _ => _.Id == PropertyId;
                var Propertyes = await _repo.GetFilteredPropertyAsync(filter);
                _logger.LogInfo($"Returned filtered Propertyes from database.");

                var PropertyResult = _mapper.Map<IEnumerable<PropertyDTO>>(Propertyes);
                return new ApiResponse<IEnumerable<PropertyDTO>> { Data = PropertyResult, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned filtered Propertyes successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredPropertysAsyncByPropertyId action: {ex.Message}");
                return new ApiResponse<IEnumerable<PropertyDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<PropertyDTO>> CreateProperty(PropertyDTO propertyObj)
        {
            PropertyDTO createdObj = null;
            try
            {
                if (propertyObj == null)
                {
                    _logger.LogError("Property object sent from client is null.");
                    return new ApiResponse<PropertyDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Property object sent from client is null" };
                }
                Expression<Func<PropertySnapshot, bool>> filter = _ => _.Name == propertyObj.Name && _.OwnerId == propertyObj.OwnerId && _.PropertyTypeId == propertyObj.PropertyTypeId;
                var Property = await _repo.GetFilteredPropertyAsync(filter);
                if (Property.Count() == 0)
                {
                    var PropertyEntity = _mapper.Map<PropertySnapshot>(propertyObj);
                    await _repo.CreateProperty(PropertyEntity);
                    createdObj = _mapper.Map<PropertyDTO>(PropertyEntity);
                    _logger.LogInfo($"Succesfully created Property with id {PropertyEntity.Id.ToString()}.");
                    return new ApiResponse<PropertyDTO> { Data = createdObj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {PropertyEntity.Id.ToString()}." };
                }
                else
                {
                    var existingObj = _mapper.Map<PropertyDTO>(Property.FirstOrDefault());
                    _logger.LogError($"Property already exists for the property with id - {Property.FirstOrDefault().Id.ToString()}.");
                    return new ApiResponse<PropertyDTO> { Data = existingObj, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Property already exists for the property with id - {Property.FirstOrDefault().Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProperty action: {ex.Message}");
                return new ApiResponse<PropertyDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<PropertyDTO>> UpdateProperty(PropertyDTO Property)
        {
            PropertyDTO updatedobj = null;
            try
            {
                if (Property == null)
                {
                    _logger.LogError("Property object sent from client is null.");
                    return new ApiResponse<PropertyDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Property object sent from client is null" };
                }

                var PropertyEntity = _mapper.Map<PropertySnapshot>(Property);

                await _repo.UpdateProperty(PropertyEntity);
                updatedobj = _mapper.Map<PropertyDTO>(PropertyEntity);

                _logger.LogInfo($"Succesfully updated Property object with id {PropertyEntity.Id.ToString()}.");
                return new ApiResponse<PropertyDTO> { Data = updatedobj, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully updated Property object with id {PropertyEntity.Id.ToString()}." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProperty action: {ex.Message}");
                return new ApiResponse<PropertyDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> DeleteProperty(int PropertyId)
        {
            try
            {
                Expression<Func<PropertySnapshot, bool>> filter = _ => _.Id == PropertyId;
                var existingObj = await _repo.GetFilteredPropertyAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"Property with id: {PropertyId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Property with id: {PropertyId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned Property with id: {PropertyId}");
                    var PropertyEntity = _mapper.Map<PropertySnapshot>(existingObj);
                    await _repo.DeleteProperty(PropertyEntity);
                    _logger.LogInfo($"Succesfully deleted Property object with id {PropertyEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted Property with id {PropertyEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProperty action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<bool>> UploadPropertyByOwner(PropertyUploadRequestModel model)
        {
            var now = DateTime.Now;
            try
            {
                if (model == null)
                {
                    _logger.LogError("Property object sent from client is null.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Property object sent from client is null" };
                }
                Expression<Func<PropertySnapshot, bool>> filter = _ => _.Name == model.PropertyName && _.OwnerId == model.OwnerId && _.PropertyTypeId == model.PropertyTypeId;
                var Property = await _repo.GetFilteredPropertyAsync(filter);
                if (Property.Count() == 0)
                {
                    //Create google map object
                    var googleMapEntity = new GoogleMapDetailsDto
                    {
                        Latitude = model.PropertyAddress.Latitude,
                        Longitude = model.PropertyAddress.Longitude,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = model.LoggedInUserId,
                        CreatedOn = now,
                        LastUpdatedBy = model.LoggedInUserId,
                        LastUpdatedOn = now
                    };
                    var createdGoogleMapObj = await _gmapService.CreateGoogleMapDetails(googleMapEntity);

                    //Create address object
                    var addressEntity = new AddressDTO
                    {
                        AddressLine1 = model.PropertyAddress.Address1,
                        AddressLine2 = model.PropertyAddress.Address2,
                        Country_State_City_Area_Ids = string.Join(';', model.PropertyAddress.AreaIds),
                        GoogleMapId = createdGoogleMapObj.Data.Id,
                        Pincode = model.PropertyAddress.Pincode,
                        PostOffice = model.PropertyAddress.PostOffice,
                        Landmark = model.PropertyAddress.Landmark,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = model.LoggedInUserId,
                        CreatedOn = now,
                        LastUpdatedBy = model.LoggedInUserId,
                        LastUpdatedOn = now
                    };
                    var createdAddressObj = await _addressService.CreateAddress(addressEntity);

                    //Create Property object
                    var PropertyEntity = new PropertyDTO
                    {
                        Name = model.PropertyName,
                        PropertyTypeId = model.PropertyTypeId,
                        PropertySubTypeId = model.PropertySubTypeId,
                        OwnerId = model.OwnerId,
                        AddressId = createdAddressObj.Data.Id,
                        ContactPersonName = model.PropertyContactName,
                        ContactEmail = model.PropertyContactEmail,
                        ContactPhoneNo = model.PropertyContactPhoneNo,
                        ContactAlternatePhoneNo = model.PropertyContactAlternatePhoneNo,
                        Description = model.PropertyDescription,
                        Rating = model.PropertyRating,
                        GSTInNo = model.PropertyGSTIn,
                        IsTerms_ConditionAccepted = model.PropertyTnCAccepted,
                        No_Of_Rooms = model.PropertyRooms.Count(),
                        PublishedOn = model.PropertyPublishDate,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = model.LoggedInUserId,
                        CreatedOn = now,
                        LastUpdatedBy = model.LoggedInUserId,
                        LastUpdatedOn = now
                    };
                    var createdProperty = await this.CreateProperty(PropertyEntity);

                    //Create Property Facility Mapping objects
                    foreach(var propfacility in model.PropertyFaciltyIds)
                    {
                        var PropertyFacilityMappingEntity = new PropertyFacilityMappingDTO
                        {
                            PropertyId = createdProperty.Data.Id,
                            FaciltiyId = propfacility,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedBy = model.LoggedInUserId,
                            CreatedOn = now,
                            LastUpdatedBy = model.LoggedInUserId,
                            LastUpdatedOn = now
                        };
                        var createdPropertyFacilityMappingObj = await _propertyFacilityMappingService.CreatePropertyFacilityMapping(PropertyFacilityMappingEntity);
                    }

                    //Create Property Policy Mapping objects
                    var PropertyPolicyMappingEntity = new PropertyPolicyMappingDTO
                    {
                        PropertyId = createdProperty.Data.Id,
                        HouseRules = model.PropertyHouseRules,
                        CancellationPolicy = model.PropertyCancellationPolicy,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = model.LoggedInUserId,
                        CreatedOn = now,
                        LastUpdatedBy = model.LoggedInUserId,
                        LastUpdatedOn = now
                    };
                    var createdPropertyPolicyMappingObj = await _propertyPolicyMappingService.CreatePropertyPolicyMapping(PropertyPolicyMappingEntity);

                    //Create Property Image Mapping objects
                    foreach (var propimage in model.PropertyImagesUrls)
                    {
                        var PropertyImageMappingEntity = new PropertyImageMappingDTO
                        {
                            PropertyId = createdProperty.Data.Id,
                            ImageUrl = propimage,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedBy = model.LoggedInUserId,
                            CreatedOn = now,
                            LastUpdatedBy = model.LoggedInUserId,
                            LastUpdatedOn = now
                        };
                        var createdPropertyImageMappingObj = await _propertImageMappingService.CreatePropertyImageMapping(PropertyImageMappingEntity);
                    }

                    //Create Room objects
                    foreach (var proproom in model.PropertyRooms)
                    {
                        var RoomEntity = new RoomDTO
                        {
                            PropertyId = createdProperty.Data.Id,
                            RoomTypeId = proproom.PropertyRoomTypeId,
                            RoomSubTypeId = proproom.PropertyRoomSubTypeId,
                            Check_In_Time = proproom.RoomCheckInTime,
                            Check_Out_Time = proproom.RoomCheckOutTime,
                            Max_No_Of_Adults =  proproom.Room_Max_No_Adults,
                            Max_No_Of_Child = proproom.Room_Max_No_Childs,
                            Name = proproom.PropertyRoomName,
                            RoomSize = proproom.PropertyRoomSize,
                            IsSlotBookingEnabled = proproom.SlotBookingEnabled,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedBy = model.LoggedInUserId,
                            CreatedOn = now,
                            LastUpdatedBy = model.LoggedInUserId,
                            LastUpdatedOn = now
                        };
                        var createdRoomObj = await _roomService.CreateRoom(RoomEntity);

                        //Create Room Facility Mapping object
                        foreach(var roomFaciity in proproom.PropertyRoomFaciltyIds)
                        {
                            var RoomFacilityMappingEntity = new RoomFacilityMappingDTO
                            {
                                RoomId = createdRoomObj.Data.Id,
                                FaciltiyId = roomFaciity,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedBy = model.LoggedInUserId,
                                CreatedOn = now,
                                LastUpdatedBy = model.LoggedInUserId,
                                LastUpdatedOn = now
                            };
                            var createdRoomFacilityMappingObj = await _roomFacilityMappingService.CreateRoomFacilityMapping(RoomFacilityMappingEntity);
                        }

                        //Create Room Image Mapping object
                        foreach (var propimage in proproom.PropertyRoomImagesUrls)
                        {
                            var RoomImageMappingEntity = new PropertyImageMappingDTO
                            {
                                PropertyId = createdProperty.Data.Id,
                                RoomId = createdRoomObj.Data.Id,
                                ImageUrl = propimage,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedBy = model.LoggedInUserId,
                                CreatedOn = now,
                                LastUpdatedBy = model.LoggedInUserId,
                                LastUpdatedOn = now
                            };
                            var createdRoomImageMappingObj = await _propertImageMappingService.CreatePropertyImageMapping(RoomImageMappingEntity);
                        }
                    }

                    
                    _logger.LogInfo($"Succesfully created Property with id {createdProperty.Data.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Role with id {createdProperty.Data.Id.ToString()}." };
                }
                else
                {
                    _logger.LogError($"Property already exists for the property with id - {Property.FirstOrDefault().Id.ToString()}.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Property already exists for the property with id - {Property.FirstOrDefault().Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProperty action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
