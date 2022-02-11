using MicroMachinesAccountService.Models;

namespace MicroMachinesAccountService.Services;

public interface IAccountRepository
{
    public Task<IEnumerable<Account>> GetAllAsync();
    public Task<IEnumerable<Account>> GetAllForUserAsync(int userId);
    public Task<Account?> GetByIdAsync(int accountId);
    public Task<Account> CreateAsync(Account account);
    public Task<bool> UpdateAsync();
    public Task<bool> DeleteAsync(int accountId);
}
