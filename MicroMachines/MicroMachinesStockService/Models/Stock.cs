namespace MicroMachinesStockService.Models
{
    public class Stock : IStock
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ItineraryItem> Balances { get; set; } = null!;
    }
}
