using MicroMachinesProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }
    }
}
