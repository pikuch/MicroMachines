using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface ITransactionService
{
    public Task<IEnumerable<TransactionReadDto>?> GetTransactionsOfUserAsync(int userId);
}
