namespace MicroMachinesCommon.Dtos;

public class StockReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<ItineraryItemReadDto> Balances { get; set; } = null!;
}
