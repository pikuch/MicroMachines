using MicroMachinesStockService.Models;

namespace MicroMachinesStockService.Services;

public interface IStockRepository
{
    public Task<IEnumerable<Stock>> GetAllAsync();
    public Task<Stock?> GetByIdAsync(int stockId);
    public Task<Stock> CreateAsync(Stock stock);
    public Task<bool> UpdateAsync();
    public Task<bool> DeleteAsync(int stockId);
    public Task<bool> AddProductsAsync(int stockId, IEnumerable<ItineraryItem> items);
    public Task<bool> RemoveProductsAsync(int stockId, IEnumerable<ItineraryItem> items);
    public Task<bool> VerifyProductsAsync(int stockId, IEnumerable<ItineraryItem> items);
}
