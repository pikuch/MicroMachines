using MicroMachinesCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class OrderUpdateDto
{
    [Required]
    public List<ItineraryItemCreateDto> Itinerary { get; set; } = null!;
    [Required]
    public int UserId { get; set; }
}
