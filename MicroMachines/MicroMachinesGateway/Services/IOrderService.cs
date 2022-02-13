using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface IOrderService
{
    public Task<IEnumerable<OrderReadDto>?> GetOrdersOfUserAsync(int userId);
    public Task<bool> CreateAsync(OrderCreateDto orderCreateDto);
}
