using MicroMachinesAccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesAccountService.Data
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
        }
    }
}
