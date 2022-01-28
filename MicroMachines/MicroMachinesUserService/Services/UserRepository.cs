using MicroMachinesUserService.Data;
using MicroMachinesUserService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesUserService.Services;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int Id)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);
    }

    public async Task<bool> UpdateAsync(int userId, User user)
    {
        return await _context.SaveChangesAsync() == 1;
    }
}
