using System.ComponentModel.DataAnnotations;

namespace MicroMachinesStockService.Models
{
    public class Stock : IStock
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;
        public List<ItineraryItem> Balances { get; set; } = null!;
    }
}
