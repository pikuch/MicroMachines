using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public interface IAccountService
{
    public Task<bool> ExecuteTransactionAsync(TransactionUpdateDto transaction);
    public Task<IEnumerable<AccountReadDto>?> GetUserAccountsAsync(int userId);
    public Task<bool> ChargeAccountAsync(int accountId, decimal orderValue);
}
