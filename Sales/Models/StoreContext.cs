using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar nombres personalizados de las tablas
            modelBuilder.Entity<Cart>().ToTable("Carts");

            // Configuraciones adicionales si lo necesitas...
        }
    }
}