using MicroMachinesStockService.Data;
using MicroMachinesStockService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesStockService.Services;

public class StockRepository : IStockRepository
{
    private readonly StockDbContext _context;

    public StockRepository(StockDbContext context)
    {
        _context = context;
    }
    
    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<bool> DeleteAsync(int stockId)
    {
        var stock = await _context.Stocks.Include(x => x.Balances).SingleOrDefaultAsync(x => x.Id == stockId);
        if (stock == null)
        {
            return false;
        }
        if (stock.Balances.Count() > 0)
        {
            return false;
        }
        _context.Stocks.Remove(stock);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<Stock>> GetAllAsync()
    {
        return await _context.Stocks.Include(x => x.Balances).AsNoTracking().ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int stockId)
    {
        return await _context.Stocks.Include(x => x.Balances).SingleOrDefaultAsync(x => x.Id == stockId);
    }

    public async Task<bool> UpdateAsync()
    {
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> AddProductsAsync(int stockId, IEnumerable<ItineraryItem> items)
    {
        var stock = await _context.Stocks.Include(x => x.Balances).SingleOrDefaultAsync(x => x.Id == stockId);
        if (stock == null)
        {
            return false;
        }
        foreach (var item in items)
        {
            var existing = stock.Balances.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing == null)
            {
                stock.Balances.Add(item);
            }
            else
            {
                existing.Count += item.Count;
            }
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveProductsAsync(int stockId, IEnumerable<ItineraryItem> items)
    {
        var stock = await _context.Stocks.Include(x => x.Balances).SingleOrDefaultAsync(x => x.Id == stockId);
        if (stock == null)
        {
            return false;
        }
        foreach (var item in items)
        {
            var existing = stock.Balances.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing == null)
            {
                return false;
            }
            if (existing.Count < item.Count)
            {
                return false;
            }

            existing.Count -= item.Count;
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> VerifyProductsAsync(int stockId, IEnumerable<ItineraryItem> items)
    {
        var stock = await _context.Stocks.Include(x => x.Balances).SingleOrDefaultAsync(x => x.Id == stockId);
        if (stock == null)
        {
            return false;
        }
        foreach (var item in items)
        {
            var existing = stock.Balances.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing == null)
            {
                return false;
            }
            if (existing.Count < item.Count)
            {
                return false;
            }

        }
        return true;
    }

}
