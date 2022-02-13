using MicroMachinesCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroMachinesCommon.Dtos;

public class OrderUpdateDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public OrderStatus OrderStatus { get; set; }
}
