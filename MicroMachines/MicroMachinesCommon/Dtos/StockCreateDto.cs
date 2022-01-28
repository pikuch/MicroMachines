using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class StockCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = null!;
}
