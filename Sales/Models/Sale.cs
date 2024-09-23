using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public required string ClientFistName { get; set; }
        public string? ClientLastName { get; set; }
        public string? ClientIDDocument { get; set; }

        public ICollection<Cart> Carts { get; set; }
    }
}