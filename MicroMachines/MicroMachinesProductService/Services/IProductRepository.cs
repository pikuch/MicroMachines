using MicroMachinesProductService.Models;

namespace MicroMachinesProductService.Services;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task<Product?> GetByIdAsync(int productId);
    public Task<Product> CreateAsync(Product product);
    public Task<bool> UpdateAsync();
    public Task<bool> DeleteAsync(int productId);
}
