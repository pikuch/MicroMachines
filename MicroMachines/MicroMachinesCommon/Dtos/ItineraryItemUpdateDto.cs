using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class ItineraryItemUpdateDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int Count { get; set; }
}
