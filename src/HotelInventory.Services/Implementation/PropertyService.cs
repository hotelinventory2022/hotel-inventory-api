using AutoMapper;
using HotelInventory.Core;
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
        private IPropertyFacilityMappingRepository _propertyFacilityRepo;
        private IPropertyPolicyMappingRepository _propertyPolicyRepo;
        private IAddressRepository _addressRepo;
        private IGoogleMapDetailsRepository _gmapRepo;
        private IAreaRepository _areaRepo;
        private IGoogleMapDetailsService _gmapService;
        private IAddressService _addressService;
        private IPropertyFacilityMappingService _propertyFacilityMappingService;
        private IPropertyPolicyMappingService _propertyPolicyMappingService;
        private IPropertyImageMappingService _propertImageMappingService;
        private IRoomService _roomService;
        private IRoomFacilityMappingService _roomFacilityMappingService;
        public PropertyService(ILoggerManager logger, IMapper mapper, IPropertyRepository repo, IPropertyFacilityMappingRepository propertyFacilityRepo, IAddressRepository addressRepo,
            IPropertyPolicyMappingRepository propertyPolicyRepo, IGoogleMapDetailsRepository gmapRepo, IAreaRepository areaRepo, IGoogleMapDetailsService gmapService, IAddressService addressService, 
            IPropertyFacilityMappingService propertyFacilityMappingService, IPropertyPolicyMappingService propertyPolicyMappingService, IPropertyImageMappingService propertImageMappingService, 
            IRoomService roomService, IRoomFacilityMappingService roomFacilityMappingService)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
            _propertyFacilityRepo = propertyFacilityRepo;
            _addressRepo = addressRepo;
            _propertyPolicyRepo = propertyPolicyRepo;
            _gmapRepo = gmapRepo;
            _areaRepo = areaRepo;
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
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created Property with id {createdProperty.Data.Id.ToString()}." };
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
        public async Task<ApiResponse<PaginatedResponseModel<List<PropertySearchResponseModel>>>> SearchProperty(PaginatedRequestModel<PropertySearchRequestModel> searchRequestModel)
        {
            IEnumerable<PropertySnapshot> properties = null;
            IEnumerable<PropertyFacilityMappingSnapshot> propertyFacilities = null;
            IEnumerable<AddressSnapshot> address = null;

            List<int> propertyIds = new List<int>();
            List<long> addressIds = new List<long>();

            Expression<Func<PropertySnapshot, bool>> propertyExpr = null;
            Expression<Func<PropertyFacilityMappingSnapshot, bool>> propertyFaciltiyExpr = null;
            Expression<Func<AddressSnapshot, bool>> addressExpr = null;
            Expression<Func<PropertyPolicyMappingSnapshot, bool>> propertyPolicyExpr = null;

            //See if address search is there, if present get the address ids
            if (searchRequestModel.RequestModel.Country_State_City_Area_Id > 0)
            {
                addressExpr = x => x.Country_State_City_Area_Ids.Contains(searchRequestModel.RequestModel.Country_State_City_Area_Id.ToString()) && x.IsActive && !x.IsDeleted;
                address = await _addressRepo.GetFilteredAddressAsync(addressExpr);
                addressIds = address.Select(s => s.Id).ToList();
            }

            //Build the search expression for property level - property type. owner, rating and address
            if (searchRequestModel.RequestModel.PropertyTypeId > 0 && searchRequestModel.RequestModel.OwnerId > 0 && !string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating) && addressIds.Count()>0)
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId 
                                        && _.OwnerId == searchRequestModel.RequestModel.OwnerId 
                                        && _.Rating == searchRequestModel.RequestModel.Rating 
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0 && searchRequestModel.RequestModel.OwnerId > 0 && !string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating))
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId 
                                        && _.OwnerId == searchRequestModel.RequestModel.OwnerId
                                        && _.Rating == searchRequestModel.RequestModel.Rating && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0 && searchRequestModel.RequestModel.OwnerId > 0 && addressIds.Count() > 0)
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId
                                        && _.OwnerId == searchRequestModel.RequestModel.OwnerId
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0 && !string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating) && addressIds.Count() > 0)
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId
                                        && _.Rating == searchRequestModel.RequestModel.Rating
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.OwnerId > 0 && !string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating) && addressIds.Count() > 0)
            {
                propertyExpr = _ => _.OwnerId == searchRequestModel.RequestModel.OwnerId
                                        && _.Rating == searchRequestModel.RequestModel.Rating
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.OwnerId > 0 && !string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating))
            {
                propertyExpr = _ => _.OwnerId == searchRequestModel.RequestModel.OwnerId 
                                        && _.Rating == searchRequestModel.RequestModel.Rating && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0 && !string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating))
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId 
                                        && _.Rating == searchRequestModel.RequestModel.Rating && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0 && searchRequestModel.RequestModel.OwnerId > 0 )
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId 
                                        && _.OwnerId == searchRequestModel.RequestModel.OwnerId && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0 && addressIds.Count() > 0)
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId 
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.OwnerId > 0 && addressIds.Count() > 0)
            {
                propertyExpr = _ => _.OwnerId == searchRequestModel.RequestModel.OwnerId 
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (!string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating) && addressIds.Count() > 0)
            {
                propertyExpr = _ => _.Rating == searchRequestModel.RequestModel.Rating 
                                        && addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.PropertyTypeId > 0)
            {
                propertyExpr = _ => _.PropertyTypeId == searchRequestModel.RequestModel.PropertyTypeId && _.IsActive && !_.IsDeleted;
            }
            else if (searchRequestModel.RequestModel.OwnerId > 0)
            {
                propertyExpr = _ => _.OwnerId == searchRequestModel.RequestModel.OwnerId && _.IsActive && !_.IsDeleted;
            }
            else if (!string.IsNullOrEmpty(searchRequestModel.RequestModel.Rating))
            {
                propertyExpr = _ => _.Rating == searchRequestModel.RequestModel.Rating && _.IsActive && !_.IsDeleted;
            }
            else if (addressIds.Count() > 0)
            {
                propertyExpr = _ => addressIds.Contains(_.AddressId) && _.IsActive && !_.IsDeleted;
            }

            //get the properties based on the search expression
            if (propertyExpr != null)
                properties = await _repo.GetFilteredPropertyAsync(propertyExpr);
            else
                properties = await _repo.GetAllPropertyesAsync();
            
            propertyIds = properties.Select(s => s.Id).ToList();

            //Get the propertyids based on facility search
            if (searchRequestModel.RequestModel.PropertyFacilities.Count() > 0) 
            {
                propertyFaciltiyExpr = s => searchRequestModel.RequestModel.PropertyFacilities.Contains(s.FaciltiyId) && propertyIds.Contains(s.PropertyId) && s.IsActive && !s.IsDeleted;
                propertyFacilities = await _propertyFacilityRepo.GetFilteredPropertyFacilityMappingAsync(propertyFaciltiyExpr);
            }
            else
            {
                propertyFaciltiyExpr = s => propertyIds.Contains(s.PropertyId) && s.IsActive && !s.IsDeleted;
                propertyFacilities = await _propertyFacilityRepo.GetFilteredPropertyFacilityMappingAsync(propertyFaciltiyExpr);
            }
            propertyIds = propertyFacilities.Select(s => s.PropertyId).ToList();

            propertyPolicyExpr = _ => propertyIds.Contains(_.PropertyId) && _.IsActive && !_.IsDeleted;
            var propertyPolicies = await _propertyPolicyRepo.GetFilteredPropertyPolicyMappingAsync(propertyPolicyExpr);

            //Get the availability
            /*To Do*/


            var searchedProperties = (from p in properties
                                     join pf in propertyFacilities on p.Id equals pf.PropertyId into ppf
                                     join pp in propertyPolicies on p.Id equals pp.PropertyId //into ppo
                                     select new PropertySearchResponseModel
                                     {
                                         PropertyId = p.Id,
                                         PropertyTypeId = p.PropertyTypeId,
                                         PropertySubTypeId = p.PropertySubTypeId,
                                         PropertyCancellationPolicy = pp.CancellationPolicy,
                                         PropertyHouseRules = pp.HouseRules,
                                         PropertyAddress = GetSearchedPropertyAdress(p.AddressId, address).Result,
                                         PropertyContactEmail = p.ContactEmail,
                                         PropertyContactAlternatePhoneNo = p.ContactAlternatePhoneNo,
                                         PropertyContactPersonName = p.ContactPersonName,
                                         PropertyContactPhoneNo = p.ContactPhoneNo,
                                         PropertyName = p.Name,
                                         PropertyOwnerId = p.OwnerId,
                                         Property_No_Of_Rooms = p.No_Of_Rooms, 
                                         //PropertyFacilities = GetPropertyFacilities(p.Id),
                                         //PropertyImageUrls = GetPropertyImages(p.Id),
                                         //PropertyRooms = GetProopertyRooms(p.Id)
                                     }).ToList();
            
            var totalRows = searchedProperties.Count();
            var totalPages = totalRows / searchRequestModel.PageSize + 1;

            return new ApiResponse<PaginatedResponseModel<List<PropertySearchResponseModel>>>
            {
                Data = new PaginatedResponseModel<List<PropertySearchResponseModel>>
                {
                    CurrentPage = searchRequestModel.CurrentPage,
                    PageSize = searchRequestModel.PageSize,
                    ResponseModel = searchedProperties,
                    TotalPages = totalPages,
                    TotalRows = totalRows
                },
                Message = $"{totalRows.ToString()} properties found!!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        private async Task<PropertySearchResponseAdrressModel> GetSearchedPropertyAdress(long AddressId, IEnumerable<AddressSnapshot> addresses =null)
        {
            PropertySearchResponseAdrressModel searchResponseAdrressModel = new PropertySearchResponseAdrressModel();
            AddressSnapshot address = new AddressSnapshot();
            if (addresses == null)
            {
                Expression<Func<AddressSnapshot, bool>> expr = _ => _.Id == AddressId && _.IsActive && !_.IsDeleted;
                address = (await _addressRepo.GetFilteredAddressAsync(expr)).FirstOrDefault();
            }
            else
                address = addresses.Where(s => s.Id == AddressId).FirstOrDefault();

            Expression<Func<GoogleMapDetailsSnapshot, bool>> gmapExpr = _ => _.Id == AddressId && _.IsActive && !_.IsDeleted;
            var googleMap = (await _gmapRepo.GetFilteredGoogleMapDetailsAsync(gmapExpr)).FirstOrDefault();

            Expression<Func<Country_State_City_AreaSnapshot, bool>> areaExpr = _ => address.Country_State_City_Area_Ids.Contains(_.Id.ToString()) && _.IsActive;
            var area = await _areaRepo.GetFilteredAreasAsync(areaExpr);

            return new PropertySearchResponseAdrressModel 
            {
                AddressId = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Landmark = address.Landmark,
                Pincode = address.Pincode,
                PostOffice = address.PostOffice,
                GoogleMapDetails = new PropertySearchResponseGoogleMapDetailsModel { GoogleMapDetailId = googleMap.Id, Latitude = googleMap.Latitude, Longitude = googleMap.Longitude},
                Areas = area.Select(s=> new PropertySearchResponseCountry_State_City_AreaModel 
                {
                    Id = s.Id,
                    CountryId = s.CountryId,
                    Country = s.CountryName,
                    StateId = s.StateId,
                    State = s.StateName,
                    CityId = s.CityId,
                    City = s.CityName,
                    AreaId = s.AreaId,
                    Area = s.AreaName
                }).ToList()
            }; 
        }
    }
}
