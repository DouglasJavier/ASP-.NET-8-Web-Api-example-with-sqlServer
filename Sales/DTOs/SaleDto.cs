using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.DTOs
{
    public class SaleGetDto
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? ClientFistName { get; set; }
        public string? ClientLastName { get; set; }
        public string? ClientIDDocument { get; set; }
        public IEnumerable<CartDto>? Carts { get; set; }

    }
    public class SaleInsertUpdateDto
    {
        [Required(ErrorMessage = "The date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The ClientFistName is required.")]
        public required string  ClientFistName { get; set; }

        [Required(ErrorMessage = "The ClientLastName is required.")]
        public  required string ClientLastName { get; set; }

        [Required(ErrorMessage = "The ClientIDDocumentis required.")]
        public required string ClientIDDocument { get; set; }
        public List<CartInsertDto> Carts { get; set; } = new List<CartInsertDto>();
    }

    public class CartDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductID { get; set; }
        public int SaleID { get; set; }
    }

    public class CartInsertDto
    {
        [Required(ErrorMessage = "The quantity is required.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "The ProductID is required.")]
        public int ProductID { get; set; }
    }
}