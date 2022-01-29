using MicroMachinesCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class TransactionUpdateDto
{
    [Required]
    public int AccountFromId { get; set; }
    [Required]
    public int AccountToId { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, 1_000_000_000)]
    public decimal Amount { get; set; }
    [Required]
    public TransactionStatus Status { get; set; }
}
