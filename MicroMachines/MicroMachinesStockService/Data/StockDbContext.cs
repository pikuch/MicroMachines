using MicroMachinesStockService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesStockService.Data
{
    public class StockDbContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; } = null!;

        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }
    }
}
