namespace MicroMachinesProductService.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
