using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.DTOs
{
    public class BrandGetDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Acronym { get; set; }
    }

    public class BrandInsertDto
    {
        public required string Name { get; set; }
        public string? Acronym { get; set; }
    }
    public class BrandUpdateDto
    {
        public required string Name { get; set; }
        public string? Acronym { get; set; }
    }
}