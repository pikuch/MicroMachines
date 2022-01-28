using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class ProductCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = null!;
    [Required]
    public int CategoryId { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}
