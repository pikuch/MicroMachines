using MicroMachinesCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class OrderCreateDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public List<ItineraryItemReadDto> Itinerary { get; set; } = null!;
}
