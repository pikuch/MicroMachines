using MicroMachinesCommon.Dtos;

namespace MicroMachinesOrderService.Services;

public interface IStockService
{
    public Task<bool> verifyStockAsync(List<ItineraryItemReadDto> itineraryItems);
}
