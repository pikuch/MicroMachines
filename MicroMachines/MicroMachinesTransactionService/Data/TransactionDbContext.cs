using MicroMachinesTransactionService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesTransactionService.Data
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; } = null!;

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }
    }
}
