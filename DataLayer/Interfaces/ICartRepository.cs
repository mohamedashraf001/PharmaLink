using DataLayer.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<CartItem?> GetByIdAsync(int id);
        Task AddAsync(CartItem item);
        void Delete(CartItem item);
        Task SaveChangesAsync();
        void Update(CartItem item);
        Task<CartItem?> GetByPharmacyAndPostAsync(int pharmacyId, int postId);
        Task<CartItem?> GetCartItemAsync(int pharmacyId, int postId);
        Task<IEnumerable<CartItem>> GetCartByPharmacyIdAsync(int pharmacyId);

    }
}
