using MicroMachinesProductService.Models;

namespace MicroMachinesProductService.Data
{
    public static class ProductDbSeeder
    {
        public static void SeedDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ProductDbContext>();

                if (context == null)
                {
                    throw new NullReferenceException(nameof(context));
                }

                var categories = new Category[]
                {
                    new Category { Name = "Fast" },
                    new Category { Name = "Heavy" },
                    new Category { Name = "Small" }
                };
                context.Categories.AddRange(categories);

                var products = new Product[]
                {
                    new Product { Name = "Car1", Category = categories[0], Price = 10M },
                    new Product { Name = "Car2", Category = categories[0], Price = 15M },
                    new Product { Name = "Car3", Category = categories[1], Price = 20M },
                    new Product { Name = "Car4", Category = categories[1], Price = 15M },
                    new Product { Name = "Car5", Category = categories[1], Price = 35M },
                    new Product { Name = "Car6", Category = categories[2], Price = 12M },
                };
                context.Products.AddRange(products);

                context.SaveChanges();
            }
        }
    }
}
