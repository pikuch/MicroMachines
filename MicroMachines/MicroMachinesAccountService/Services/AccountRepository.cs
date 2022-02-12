using MicroMachinesAccountService.Data;
using MicroMachinesAccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesAccountService.Services;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _context;

    public AccountRepository(AccountDbContext context)
    {
        _context = context;
    }
    public async Task<Account> CreateAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAsync(int accountId)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == accountId);
        if (account == null)
        {
            return false;
        }
        _context.Accounts.Remove(account);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetAllForUserAsync(int userId)
    {
        return await _context.Accounts.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.Id == accountId);
    }

    public async Task<bool> UpdateAsync()
    {
        return await _context.SaveChangesAsync() >= 1;
    }
}
