using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public interface IProductService
{
    public Task<decimal?> GetItineraryValueAsync(List<ItineraryItemReadDto> itineraryItems);
}
