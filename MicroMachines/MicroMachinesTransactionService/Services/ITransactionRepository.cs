using MicroMachinesTransactionService.Models;

namespace MicroMachinesTransactionService.Services;

public interface ITransactionRepository
{
    public Task<IEnumerable<Transaction>> GetAllAsync();
    public Task<IEnumerable<Transaction>> GetForUserAsync(int userId);
    public Task<Transaction?> GetByIdAsync(int transactionId);
    public Task<Transaction> CreateAsync(Transaction transaction);
    public Task<bool> UpdateAsync();
    public Task<bool> DeleteAsync(int transactionId);
}
