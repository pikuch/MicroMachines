using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class UserCreateDto
{
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
}
