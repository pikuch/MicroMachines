using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesStockService.Models;

namespace MicroMachinesStockService.Data;

public class StockServiceAutomapperProfile : Profile
{
    public StockServiceAutomapperProfile()
    {
        CreateMap<StockCreateDto, Stock>();
        CreateMap<Stock, StockReadDto>();
        CreateMap<ItineraryItemCreateDto, ItineraryItem>();
        CreateMap<ItineraryItem, ItineraryItemReadDto>();
    }
}
