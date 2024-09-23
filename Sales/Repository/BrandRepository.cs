using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class BrandRepository : IRepository<Brand>
    {
        private StoreContext _context;
        public BrandRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brand>> Get()
        {
            var result = await _context.Brands.ToListAsync();
            return result;
        }

        public async Task<Brand?> GetById(int id)
        {
            var result = await _context.Brands.FindAsync(id);
            return result;
        }

        public async Task Add(Brand Brand)
        => await _context.Brands.AddAsync(Brand);

        public void Update(Brand Brand)
        {
            _context.Brands.Attach(Brand);
            _context.Brands.Entry(Brand).State = EntityState.Modified;
        }
        public void Delete(Brand Brand)
        {
            _context.Brands.Remove(Brand);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Brand> Search(Func<Brand, bool> filter)
        {
            return _context.Brands.Where(filter).ToList();
        }
    }
}