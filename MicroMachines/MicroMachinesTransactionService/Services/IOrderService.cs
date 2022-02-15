namespace MicroMachinesTransactionService.Services;

public interface IOrderService
{
    public Task<bool> ConfirmOrder(int orderId, int transactionId);
}
