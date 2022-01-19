namespace MicroMachinesStockService.Models
{
    public interface IStock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItineraryItem> Balances { get; set; }
    }
}
