using System.ComponentModel.DataAnnotations;

namespace MicroMachinesAccountService.Models
{
    public class Account : IAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;
        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
        [Required]
        public bool IsClosed { get; set; }
    }
}
