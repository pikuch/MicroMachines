using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public interface IAccountService
{
    public Task<bool> ExecuteTransactionAsync(TransactionUpdateDto transaction);
}
