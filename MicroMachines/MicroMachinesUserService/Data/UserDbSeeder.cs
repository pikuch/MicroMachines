using MicroMachinesUserService.Models;

namespace MicroMachinesUserService.Data
{
    public static class UserDbSeeder
    {
        public static void SeedDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UserDbContext>();

                if (context == null)
                {
                    throw new NullReferenceException(nameof(context));
                }

                var userProducts1 = new List<ItineraryItem>()
                {
                    new ItineraryItem { ProductId = 1, Count = 2 },
                    new ItineraryItem { ProductId = 2, Count = 1 },
                    new ItineraryItem { ProductId = 5, Count = 3 },
                    new ItineraryItem { ProductId = 6, Count = 1 }
                };
                var userProducts2 = new List<ItineraryItem>()
                {
                    new ItineraryItem { ProductId = 1, Count = 1 },
                    new ItineraryItem { ProductId = 3, Count = 1 },
                    new ItineraryItem { ProductId = 4, Count = 2 }
                };

                var users = new User[]
                {
                    new User() {
                        FirstName = "Adam",
                        LastName = "Adamiak",
                        Address = "Amary street 10",
                        Department = "The only department",
                        Email = "Adam@aol.com",
                        PhoneNo = "123456789",
                        Products = userProducts1
                        },
                    new User() {
                        FirstName = "Beata",
                        LastName = "Bogara",
                        Address = "Botka street 20",
                        Department = "The only department",
                        Email = "Beata@aol.com",
                        PhoneNo = "987654321",
                        Products = userProducts2
                        }
                };
                context.Users.AddRange(users);

                context.SaveChanges();
            }
        }
    }
}
