using System.ComponentModel.DataAnnotations;

namespace MicroMachinesProductService.Models
{
    public class Category : ICategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;
    }
}
