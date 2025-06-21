using DataAccessLayer.Data;
using DataLayer.Entities;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItems
                .Include(c => c.Pharmacy)
                .Include(c => c.Post)
                .ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _context.CartItems
                .Include(c => c.Pharmacy)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
        }

        public void Delete(CartItem item)
        {
            _context.CartItems.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(CartItem item)
        {
            _context.CartItems.Update(item);
        }


        public async Task<CartItem?> GetByPharmacyAndPostAsync(int pharmacyId, int postId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c => c.PharmacyId == pharmacyId && c.PostId == postId);
        }
        public async Task<CartItem?> GetCartItemAsync(int pharmacyId, int postId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c => c.PharmacyId == pharmacyId && c.PostId == postId);
        }
        public async Task<IEnumerable<CartItem>> GetCartByPharmacyIdAsync(int pharmacyId)
        {
            return await _context.CartItems
                .Include(ci => ci.Post)
                .ThenInclude(p => p.Pharmacy)
                .Where(ci => ci.PharmacyId == pharmacyId)
                .ToListAsync();
        }

    }
}
