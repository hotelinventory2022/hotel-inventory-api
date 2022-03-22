using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.Models;
using HotelInventory.Models.Room;
using HotelInventory.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Services.Implementation
{
    public class AvailabilityService : IAvailabilityService
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IAvailabilityRepository _repo;
        private IPropertyRepository _propertyRepo;
        private IRoomRepository _roomRepo;
        public AvailabilityService(ILoggerManager logger, IMapper mapper, IAvailabilityRepository repo, IPropertyRepository propertyRepo, IRoomRepository roomRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
            _propertyRepo = propertyRepo;
            _roomRepo = roomRepo;
        }
        public async Task<ApiResponse<IEnumerable<AvailabiltyDTO>>> GetAvailibiltyAsync(AvailabiltyRequestModel availabilty)
        {
            try
            {
                IEnumerable<AvailabiltyDTO> listAvailabiltyDTOs;
                List<int> propertyIds;
                List<int> roomIds;
                if (availabilty.PropertyIds == null || availabilty.PropertyIds.Count == 0)
                {
                    Expression<Func<PropertySnapshot, bool>> propertyFilter = _ => _.OwnerId == availabilty.OwnerId;
                    var ownerProperty = await _propertyRepo.GetFilteredPropertyAsync(propertyFilter);

                    propertyIds = ownerProperty.Select(_ => _.Id).ToList();
                }
                else
                    propertyIds = availabilty.PropertyIds;

                if (availabilty.ListOfAvailabilties == null || availabilty.ListOfAvailabilties.Count == 0)
                {
                    Expression<Func<RoomSnapshot, bool>> roomFilter = _ => propertyIds.Contains(_.PropertyId);
                    var ownerRooms = await _roomRepo.GetFilteredRoomAsync(roomFilter);

                    roomIds = ownerRooms.Select(_ => _.Id).ToList();
                }
                else
                    roomIds = availabilty.ListOfAvailabilties.Select(_ => _.RoomId).ToList();

                Expression<Func<AvailabiltyRateSnapshot, bool>> avlRateFilter = _ => roomIds.Contains(_.RoomId) && _.StartDate >= availabilty.StartDate && _.StartDate <= availabilty.EndDate;
                var availibilty = await _repo.GetFilteredAvailabilityAsync(avlRateFilter);

                listAvailabiltyDTOs = _mapper.Map<IEnumerable<AvailabiltyDTO>>(availibilty);

                _logger.LogInfo($"Returned availibilty for owner {availabilty.OwnerId} from database ");
                return new ApiResponse<IEnumerable<AvailabiltyDTO>> { Data = listAvailabiltyDTOs, StatusCode = System.Net.HttpStatusCode.OK, Message = "Returned all availibilty successfully from database." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAvailibiltyAsync action: {ex.Message}");
                return new ApiResponse<IEnumerable<AvailabiltyDTO>> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
        public async Task<ApiResponse<AvailabiltyDTO>> CreateOrUpdateAvailibilty(AvailabiltyRequestModel Availibilty)
        {
            try
            {
                List<Task> dbTask = new List<Task>();
                List<AvailabiltyRateSnapshot> createList = new List<AvailabiltyRateSnapshot>();
                foreach (var slot in Availibilty.ListOfAvailabilties)
                {
                    if (slot.Id > 0)
                    {
                        dbTask.Add(UpdateAvailibilty(slot, Availibilty.LoggedInUserId));
                    }
                    else 
                    {
                        var availibiltyEntity = _mapper.Map<AvailabiltyRateSnapshot>(slot);
                        availibiltyEntity.CreatedBy = Availibilty.LoggedInUserId;
                        availibiltyEntity.CreatedOn = DateTime.Now;
                        availibiltyEntity.IsActive = true;
                        availibiltyEntity.LastUpdatedBy = Availibilty.LoggedInUserId;
                        availibiltyEntity.LastUpdatedOn = DateTime.Now;
                        createList.Add(availibiltyEntity);
                    }                    
                }
                dbTask.Add(_repo.CreateAvailability(createList));
                await Task.WhenAll(dbTask);

                _logger.LogInfo($"Succesfully created availibilty for Owner {Availibilty.OwnerId}.");
                return new ApiResponse<AvailabiltyDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully created availibilty for Owner {Availibilty.OwnerId}." };
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAvailibilty action: {ex.Message}");
                return new ApiResponse<AvailabiltyDTO> { Data = null, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }

        private async Task UpdateAvailibilty(AvailabiltyDTO slot, int loggedInUserId)
        {
            try
            {
                Expression<Func<AvailabiltyRateSnapshot, bool>> filter = _ => _.Id == slot.Id;
                var availibiltyEntity = await _repo.GetFilteredAvailabilityAsync(filter);
                
                var availibiltyEntityToUpdate = _mapper.Map<AvailabiltyRateSnapshot>(slot);
                availibiltyEntityToUpdate.CreatedBy = availibiltyEntity.FirstOrDefault().CreatedBy;
                availibiltyEntityToUpdate.CreatedOn = availibiltyEntity.FirstOrDefault().CreatedOn;
                availibiltyEntityToUpdate.IsActive = availibiltyEntity.FirstOrDefault().IsActive;
                availibiltyEntityToUpdate.IsDeleted = availibiltyEntity.FirstOrDefault().IsDeleted;
                availibiltyEntityToUpdate.LastUpdatedBy = loggedInUserId;
                availibiltyEntityToUpdate.LastUpdatedOn = DateTime.Now;

                 await _repo.UpdateAvailability(availibiltyEntityToUpdate);
                _logger.LogInfo($"Succesfully updated availibilty object with id {availibiltyEntityToUpdate.Id}.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAvailibilty action: {ex.Message}");
            }
        }


        public async Task<ApiResponse<bool>> DeleteAvailibilty(int AvailibiltyId)
        {
            try
            {
                Expression<Func<AvailabiltyRateSnapshot, bool>> filter = _ => _.Id == AvailibiltyId;
                var existingObj = await _repo.GetFilteredAvailabilityAsync(filter);
                if (existingObj == null)
                {
                    _logger.LogError($"Availibilty with id: {AvailibiltyId}, hasn't been found in db.");
                    return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Availibilty with id: {AvailibiltyId}, hasn't been found." };
                }
                else
                {
                    _logger.LogInfo($"Returned Availibilty with id: {AvailibiltyId}");
                    var availibiltyIdEntity = _mapper.Map<AvailabiltyRateSnapshot>(existingObj);
                    await _repo.DeleteAvailability(availibiltyIdEntity);
                    _logger.LogInfo($"Succesfully deleted Availibilty object with id {availibiltyIdEntity.Id.ToString()}.");
                    return new ApiResponse<bool> { Data = true, StatusCode = System.Net.HttpStatusCode.OK, Message = $"Succesfully deleted Availibilty with id {availibiltyIdEntity.Id.ToString()}." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAvailibilty action: {ex.Message}");
                return new ApiResponse<bool> { Data = false, StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "500 Internal Server Error - Something went wrong." };
            }
        }
    }
}
