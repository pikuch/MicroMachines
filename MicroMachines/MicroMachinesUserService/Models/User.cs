namespace MicroMachinesUserService.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<ItineraryItem> Products { get; set; } = null!;
    }
}
