using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesOrderService.Models;

namespace MicroMachinesOrderService.Data;

public class OrderServiceAutomapperProfile : Profile
{
    public OrderServiceAutomapperProfile()
    {
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
        CreateMap<Order, OrderReadDto>();
        CreateMap<ItineraryItemCreateDto, ItineraryItem>();
        CreateMap<ItineraryItemUpdateDto, ItineraryItem>();
        CreateMap<ItineraryItem, ItineraryItemReadDto>();
        CreateMap<ItineraryItemReadDto, ItineraryItem>();
    }
}
