using MicroMachinesAccountService.Models;

namespace MicroMachinesAccountService.Data
{
    public static class AccountDbSeeder
    {
        public static void SeedDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AccountDbContext>();

                if (context == null)
                {
                    throw new NullReferenceException(nameof(context));
                }

                var accounts = new Account[]
                {
                    new Account { UserId = 1, Name = "My first account", Balance = 3000M, IsClosed = false},
                    new Account { UserId = 1, Name = "My second account", Balance = 2000M, IsClosed = false},
                    new Account { UserId = 2, Name = "My account", Balance = 10000M, IsClosed = false},
                };
                context.Accounts.AddRange(accounts);

                context.SaveChanges();
            }
        }
    }
}
