using MicroMachinesCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesOrderService.Models
{
    public class Order : IOrder
    {
        [Key]
        public int Id { get; set; }
        public List<ItineraryItem> Itinerary { get; set; } = null!;
        [Required]
        public int UserId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }
        public int? TransactionId { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
