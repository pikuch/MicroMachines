using MicroMachinesCommon.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesOrderService.Models
{
    public class ItineraryItem : IItineraryItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
