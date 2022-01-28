using MicroMachinesCommon.Dtos;
using MicroMachinesUserService.Models;

namespace MicroMachinesUserService.Services;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(int userId);
    public Task<User> CreateAsync(User user);
    public Task<bool> UpdateAsync(int userId, User user);
    public Task<bool> DeleteAsync(int userId);
    public Task<IEnumerable<ItineraryItem>?> GetProductsAsync(int userId);
    public Task<bool> AddProductsAsync(int userId, IEnumerable<ItineraryItem> items);
}
