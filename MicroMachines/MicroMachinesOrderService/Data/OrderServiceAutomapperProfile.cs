using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesOrderService.Models;

namespace MicroMachinesOrderService.Data;

public class OrderServiceAutomapperProfile : Profile
{
    public OrderServiceAutomapperProfile()
    {
        CreateMap<OrderCreateDto, Order>();
        CreateMap<Order, OrderReadDto>();
        CreateMap<ItineraryItemCreateDto, ItineraryItem>();
        CreateMap<ItineraryItem, ItineraryItemReadDto>();
    }
}
