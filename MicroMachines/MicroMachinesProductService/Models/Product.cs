using System.ComponentModel.DataAnnotations;

namespace MicroMachinesProductService.Models
{
    public class Product : IProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 1_000_000_000)]
        public decimal Price { get; set; }
    }
}
