using MicroMachinesProductService.Data;
using MicroMachinesProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesProductService.Services;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }
    public async Task<Product> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
        if (product == null)
        {
            return false;
        }
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
    }

    public async Task<bool> UpdateAsync(int productId, Product product)
    {
        return await _context.SaveChangesAsync() == 1;
    }
}
