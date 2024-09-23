using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> Get()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product?> GetById(int id)
        {
            var result = await _context.Products.FindAsync(id);
            return result;
        }

        public async Task Add(Product product)
        => await _context.Products.AddAsync(product);

        public void Update(Product product)
        {
            _context.Products.Attach(product);
            _context.Products.Entry(product).State = EntityState.Modified;
        }
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Product> Search(Func<Product, bool> filter)
        {
            return _context.Products.Where(filter).ToList();
        }
    }
}