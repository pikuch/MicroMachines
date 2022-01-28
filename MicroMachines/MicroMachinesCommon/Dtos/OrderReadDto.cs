using MicroMachinesCommon.Enums;

namespace MicroMachinesCommon.Dtos;

public class OrderReadDto
{
    public int Id { get; set; }
    public List<ItineraryItemReadDto> Itinerary { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int TransactionId { get; set; }
    public OrderStatus Status { get; set; }
}
