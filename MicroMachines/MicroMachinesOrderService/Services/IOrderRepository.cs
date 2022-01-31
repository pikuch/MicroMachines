using MicroMachinesOrderService.Models;

namespace MicroMachinesOrderService.Services;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllAsync();
    public Task<IEnumerable<Order>> GetForUserAsync(int userId);
    public Task<Order?> GetByIdAsync(int orderId);
    public Task<Order> CreateAsync(Order order);
    public Task<bool> UpdateAsync();
    public Task<bool> DeleteAsync(int orderId);
    public Task<bool> AddItemsAsync(int orderId, IEnumerable<ItineraryItem> items);
}
