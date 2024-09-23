using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Model { get; set; }
        public int Stock { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? BrandID{ get; set; }

        [ForeignKey("BrandID")]
        public virtual Brand? Brand { get; set; }
    }
}