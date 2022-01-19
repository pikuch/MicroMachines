using MicroMachinesStockService.Models;

namespace MicroMachinesStockService.Data
{
    public static class StockDbSeeder
    {
        public static void SeedDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<StockDbContext>();

                if (context == null)
                {
                    throw new NullReferenceException(nameof(context));
                }

                var balances = new List<ItineraryItem>()
                {
                    new ItineraryItem { ProductId = 1, Count = 100 },
                    new ItineraryItem { ProductId = 2, Count = 110 },
                    new ItineraryItem { ProductId = 3, Count = 120 },
                    new ItineraryItem { ProductId = 4, Count = 130 },
                    new ItineraryItem { ProductId = 5, Count = 140 },
                    new ItineraryItem { ProductId = 6, Count = 150 }
                };

                var stock = new Stock { Name = "Main stock", Balances = balances };
                
                context.Stocks.Add(stock);

                context.SaveChanges();
            }
        }
    }
}
