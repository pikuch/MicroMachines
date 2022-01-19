using MicroMachinesCommon.Enums;

namespace MicroMachinesTransactionService.Models
{
    public class Transaction : ITransaction
    {
        public int Id { get; set; }
        public int AccountFromId { get; set; }
        public int AccountToId { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
