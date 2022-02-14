using MicroMachinesCommon.Dtos;

namespace MicroMachinesOrderService.Services;

public interface IPaymentQueue
{
    public Task<bool> Enqueue(OrderReadDto order);
}
