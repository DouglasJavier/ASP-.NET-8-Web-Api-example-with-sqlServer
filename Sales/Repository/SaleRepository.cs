using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class SaleRepository : IRepository<Sale>
    {
        private readonly StoreContext _context;

        public SaleRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> Get()
        {
            return await _context.Sales.Include(s => s.Carts).ToListAsync();
        }

        public async Task<Sale?> GetById(int id)
        {
            return await _context.Sales.Include(s => s.Carts)
                                       .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Add(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public void Update(Sale sale)
        {
            _context.Sales.Attach(sale);
            _context.Sales.Entry(sale).State = EntityState.Modified;
        }

        public void Delete(Sale sale)
        {
            _context.Sales.Remove(sale);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Sale> Search(Func<Sale, bool> filter)
        {
            return _context.Sales.Include(s => s.Carts).Where(filter).ToList();
        }
    }
}
