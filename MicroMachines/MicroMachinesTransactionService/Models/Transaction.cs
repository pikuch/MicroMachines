using MicroMachinesCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesTransactionService.Models
{
    public class Transaction : ITransaction
    {
        [Key]
        public int Id { get; set; }
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
}
