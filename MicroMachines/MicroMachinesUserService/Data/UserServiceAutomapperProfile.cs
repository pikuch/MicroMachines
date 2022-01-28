using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesUserService.Models;

namespace MicroMachinesUserService.Data;

public class UserServiceAutomapperProfile : Profile
{
    public UserServiceAutomapperProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserReadDto>();
        CreateMap<ItineraryItemCreateDto, ItineraryItem>();
        CreateMap<ItineraryItem, ItineraryItemReadDto>();
    }
}
