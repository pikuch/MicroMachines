using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class ProductUpdateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = null!;
    [Required]
    public int CategoryId { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, 1_000_000_000)]
    public decimal Price { get; set; }
}
