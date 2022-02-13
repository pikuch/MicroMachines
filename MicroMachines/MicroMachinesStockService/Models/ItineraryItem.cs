using MicroMachinesCommon.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesStockService.Models
{
    public class ItineraryItem : IItineraryItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
    }
}
