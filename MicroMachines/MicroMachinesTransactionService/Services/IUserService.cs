using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public interface IUserService
{
    public Task<bool> AddProducts(int userId, IEnumerable<ItineraryItemReadDto> itineraryItems);
}
