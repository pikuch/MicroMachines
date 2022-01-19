﻿namespace MicroMachinesProductService.Models
{
    public interface IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
