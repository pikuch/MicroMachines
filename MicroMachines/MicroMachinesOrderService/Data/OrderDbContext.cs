using MicroMachinesOrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesOrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }
    }
}
