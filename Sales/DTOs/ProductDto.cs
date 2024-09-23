using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.DTOs
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int Stock { get; set; }

        public decimal Price { get; set; }
        public int BrandID { get; set; }
        //public required Brand Brand { get; set; }
    }

    public class ProductInsertDto
    {
        public required string Name { get; set; }
        public string? Model { get; set; }
        public int Stock { get; set; }

        public decimal Price { get; set; }

        public int BrandID { get; set; }
    }
    public class ProductUpdateDto
    {
        public required string Name { get; set; }
        public string? Model { get; set; }
        public int Stock { get; set; }

        public decimal Price { get; set; }

        public int BrandID { get; set; }
    }
}