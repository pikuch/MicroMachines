﻿namespace MicroMachinesUserService.Models
{
    public interface IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public List<ItineraryItem> Products { get; set; }
    }
}
