namespace MicroMachinesProductService.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
