using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public interface IStockService
{
    public Task<bool> RemoveProducts(IEnumerable<ItineraryItemReadDto> itineraryItems);
}
