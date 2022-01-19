using MicroMachinesUserService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMachinesUserService.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
    }
}
