using MicroMachinesOrderService.Data;
using MicroMachinesOrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesOrderService.Services;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }
    public async Task<Order> CreateAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteAsync(int orderId)
    {
        var order = await _context.Orders.Include(x => x.Itinerary).SingleOrDefaultAsync(x => x.Id == orderId);
        if (order == null)
        {
            return false;
        }
        if (order.Itinerary.Count > 0)
        {
            return false;
        }
        _context.Orders.Remove(order);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.Include(x => x.Itinerary).AsNoTracking().ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int orderId)
    {
        return await _context.Orders.Include(x => x.Itinerary).SingleOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task<bool> UpdateAsync()
    {
        return await _context.SaveChangesAsync() >= 1;
    }

    public async Task<IEnumerable<Order>> GetForUserAsync(int userId)
    {
        return await _context.Orders.Include(x => x.Itinerary)
            .Where(x => x.UserId == userId)
            .AsNoTracking().ToListAsync();
    }

    public async Task<bool> AddItemsAsync(int orderId, IEnumerable<ItineraryItem> items)
    {
        var order = await _context.Orders.Include(x => x.Itinerary).SingleOrDefaultAsync(x => x.Id == orderId);
        if (order == null)
        {
            return false;
        }

        foreach (var item in items)
        {
            var existing = order.Itinerary.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing == null)
            {
                order.Itinerary.Add(item);
            }
            else
            {
                existing.Count += item.Count;
            }
        }
        await _context.SaveChangesAsync();
        return true;
    }
}
