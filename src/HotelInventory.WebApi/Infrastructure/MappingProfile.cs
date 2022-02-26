using AutoMapper;
using HotelInventory.Core.Domains;
using HotelInventory.Models.Roles;

namespace HotelInventory.WebApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleSnapshot, RoleDto>();

            CreateMap<RoleDto, RoleSnapshot>();
        }
    }
}
