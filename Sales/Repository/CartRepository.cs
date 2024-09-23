using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class CartRepository
    {
        private StoreContext _context;
        public CartRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(Cart Cart)
        => await _context.Carts.AddAsync(Cart);

        public void Delete(Cart Cart)
        {
            _context.Carts.Remove(Cart);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}