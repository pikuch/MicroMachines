using System.ComponentModel.DataAnnotations;

namespace MicroMachinesUserService.Models
{
    public class User : IUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string PhoneNo { get; set; } = null!;
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Department { get; set; } = null!;
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Address { get; set; } = null!;
        public List<ItineraryItem> Products { get; set; } = null!;
    }
}
