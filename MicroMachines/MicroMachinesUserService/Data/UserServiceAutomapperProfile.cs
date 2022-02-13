using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesUserService.Models;

namespace MicroMachinesUserService.Data;

public class UserServiceAutomapperProfile : Profile
{
    public UserServiceAutomapperProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserReadDto>();
        CreateMap<ItineraryItemCreateDto, ItineraryItem>();
        CreateMap<ItineraryItemUpdateDto, ItineraryItem>();
        CreateMap<ItineraryItem, ItineraryItemReadDto>();
    }
}
