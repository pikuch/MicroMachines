using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface IUserService
{
    public Task<IEnumerable<ItineraryItemReadDto>?> GetProductsOfUserAsync(int userId);
}
