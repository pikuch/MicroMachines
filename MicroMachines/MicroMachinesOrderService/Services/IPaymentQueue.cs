namespace MicroMachinesOrderService.Services;

public interface IPaymentQueue
{
    public Task<bool> Enqueue(int orderId);
}
