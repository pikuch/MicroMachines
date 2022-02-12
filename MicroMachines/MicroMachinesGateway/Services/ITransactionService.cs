using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface ITransactionService
{
    public Task<IEnumerable<TransactionReadDto>?> GetTransactionsOfUserAsync(int userId);
    public Task<bool> CreateAsync(TransactionCreateDto transaction);
    public Task<bool> ConfirmAsync(int transactionId);
}
