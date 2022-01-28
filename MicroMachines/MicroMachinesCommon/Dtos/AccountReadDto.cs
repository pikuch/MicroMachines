namespace MicroMachinesCommon.Dtos;

public class AccountReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsClosed { get; set; }
}
