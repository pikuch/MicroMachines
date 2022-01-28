using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace MicroMachinesCommon.Dtos;

public class TransactionCreateDto
{
    [Required]
    public int AccountFromId { get; set; }
    [Required]
    public int AccountToId { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime TimeStamp { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }
    [Required]
    public TransactionStatus Status { get; set; }
}
