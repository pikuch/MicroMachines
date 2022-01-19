using MicroMachinesCommon.Interfaces;

namespace MicroMachinesUserService.Models
{
    public class ItineraryItem : IItineraryItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
