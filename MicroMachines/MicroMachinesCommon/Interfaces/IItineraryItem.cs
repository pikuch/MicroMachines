namespace MicroMachinesCommon.Interfaces
{
    public interface IItineraryItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
