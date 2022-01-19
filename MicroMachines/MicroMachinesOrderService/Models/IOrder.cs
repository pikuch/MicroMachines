using MicroMachinesCommon.Enums;

namespace MicroMachinesOrderService.Models
{
    public interface IOrder
    {
        public int Id { get; set; }
        public List<ItineraryItem> Itinerary { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int TransactionId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
