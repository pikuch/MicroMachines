namespace MicroMachinesAccountService.Models
{
    public interface IAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public bool IsClosed { get; set; }
    }
}
