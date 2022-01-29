using MicroMachinesTransactionService.Data;
using MicroMachinesTransactionService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesTransactionService.Services;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionDbContext _context;

    public TransactionRepository(TransactionDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> DeleteAsync(int transactionId)
    {
        var transaction = await _context.Transactions.SingleOrDefaultAsync(x => x.Id == transactionId);
        if (transaction == null)
        {
            return false;
        }
        _context.Transactions.Remove(transaction);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.AsNoTracking().ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int transactionId)
    {
        return await _context.Transactions.SingleOrDefaultAsync(x => x.Id == transactionId);
    }

    public async Task<IEnumerable<Transaction>> GetForUserAsync(int userId)
    {
        return await _context.Transactions
            .Where(x => x.AccountFromId == userId || x.AccountToId == userId)
            .AsNoTracking().ToListAsync();
    }

    public async Task<bool> UpdateAsync()
    {
        return await _context.SaveChangesAsync() == 1;
    }
}
