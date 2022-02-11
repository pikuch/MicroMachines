using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface IAccountService
{
    public Task<IEnumerable<AccountReadDto>?> GetAccountsOfUserAsync(int userId);
}
