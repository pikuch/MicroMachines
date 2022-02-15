using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesStockService.Models;

namespace MicroMachinesStockService.Data;

public class StockServiceAutomapperProfile : Profile
{
    public StockServiceAutomapperProfile()
    {
        CreateMap<StockCreateDto, Stock>();
        CreateMap<StockUpdateDto, Stock>();
        CreateMap<Stock, StockReadDto>();
        CreateMap<ItineraryItemCreateDto, ItineraryItem>();
        CreateMap<ItineraryItemUpdateDto, ItineraryItem>();
        CreateMap<ItineraryItemReadDto, ItineraryItem>();
        CreateMap<ItineraryItem, ItineraryItemReadDto>();
    }
}
